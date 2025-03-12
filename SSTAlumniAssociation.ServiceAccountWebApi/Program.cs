using FastEndpoints;
using FastEndpoints.ClientGen.Kiota;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Kiota.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using Scalar.AspNetCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceAccountWebApi.Authorization;
using SSTAlumniAssociation.ServiceAccountWebApi.Authorization.ServiceAccount;
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

builder.Services.AddScoped<IAuthorizationHandler, ServiceAccountHandler>();

#endregion

#region Auth

builder.Services.AddAuthenticationJwtBearer(
    _ => { },
    options =>
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
    .AddPolicy(Policies.ServiceAccount, policy =>
        policy.AddRequirements(new ServiceAccountRequirement())
    );

#endregion

#region FastEndpoints

builder.Services.AddFastEndpoints()
    .SwaggerDocument(options =>
    {
        options.MaxEndpointVersion = 1;
        options.UseOneOfForPolymorphism = true;
        options.DocumentSettings = s =>
        {
            s.Title = "SST Alumni Association Service Account API";
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
            builder.Environment.IsDevelopment() ? "http://localhost:3000" : "https://*.sstaa.pages.dev"
        );
    });
});

#endregion

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseFastEndpoints(options => { options.Versioning.Prefix = "v"; })
    .UseSwaggerGen(options => { options.Path = "/openapi/{documentName}.json"; });

await app.GenerateApiClientsAndExitAsync(
    c =>
    {
        c.SwaggerDocumentName = "v1";
        c.Language = GenerationLanguage.TypeScript;
        c.OutputPath = Path.Combine("..", "SSTAlumniAssociation.WebApp", "api", "service-account");
    });

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapScalarApiReference();

app.Run();
