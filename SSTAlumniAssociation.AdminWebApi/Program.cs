using System.Text.Json.Serialization;
using FastEndpoints;
using FastEndpoints.ClientGen.Kiota;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Kiota.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using NSwag;
using Scalar.AspNetCore;
using SSTAlumniAssociation.AdminWebApi.Authorization;
using SSTAlumniAssociation.AdminWebApi.Authorization.Admin;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

#region Database

if (builder.IsApiClientGenerationMode())
{
    EF.IsDesignTime = true;
}

builder.AddNpgsqlDbContext<AppDbContext>("sstaa",
    configureDbContextOptions: options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        }

        options.UseNpgsql(o =>
            o.MigrationsAssembly("SSTAlumniAssociation.Migrations")
                .MapEnum<ServiceAccountType>()
                .MapEnum<PaymentIntentState>()
        );
    });

#endregion

#region Services

builder.Services.AddScoped<IAuthorizationHandler, AdminRequirementExcoHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AdminRequirementSystemAdminHandler>();

#endregion

#region Auth

builder.Services
    .AddAuthenticationJwtBearer(s => { }, options =>
    {
        var projectId = builder.Configuration.GetValue<string>("Firebase:ProjectId");
        options.Authority = $"https://securetoken.google.com/{projectId}";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = $"https://securetoken.google.com/{projectId}",
            ValidAudience = projectId,
            ValidateIssuerSigningKey = true,
            ValidateTokenReplay = true
        };
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Policies.Admin, policy => { policy.AddRequirements(new AdminRequirement()); });

#endregion

#region FastEndpoints

builder.Services.AddFastEndpoints()
    .SwaggerDocument(options =>
    {
        options.MaxEndpointVersion = 1;
        options.UseOneOfForPolymorphism = true;
        options.DocumentSettings = s =>
        {
            s.Title = "SST Alumni Association Admin API";
            s.Version = "v1";

            s.AddAuth("Bearer", new NSwag.OpenApiSecurityScheme
            {
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Firebase ID Token",
                Name = "Authorization",
                Type = OpenApiSecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
        };
    });

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowCredentials();
        policy.AllowAnyMethod();
        policy.WithHeaders("Authorization", "Content-Type", "User-Agent");
        policy.WithOrigins(
            "https://app.sstaa.org",
            "http://localhost:3000"
        );
    });
});

#endregion

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(options => { options.Versioning.Prefix = "v"; })
    .UseSwaggerGen(options => { options.Path = "/openapi/{documentName}.json"; });

await app.GenerateApiClientsAndExitAsync(
    c =>
    {
        c.SwaggerDocumentName = "v1";
        c.Language = GenerationLanguage.TypeScript;
        c.OutputPath = Path.Combine("..", "SSTAlumniAssociation.WebApp", "api", "admin");
    });

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();

    app.MapScalarApiReference();
}

app.UseCors();

app.UseHttpsRedirection();

app.Run();