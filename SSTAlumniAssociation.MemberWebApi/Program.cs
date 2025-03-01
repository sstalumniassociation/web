using FastEndpoints;
using FastEndpoints.ClientGen.Kiota;
using FastEndpoints.Swagger;
using Kiota.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceDefaults;
using SSTAlumniAssociation.MemberWebApi.Authorization;
using SSTAlumniAssociation.MemberWebApi.Authorization.Member;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

#region Database

EF.IsDesignTime = builder.IsApiClientGenerationMode();
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

builder.Services.AddScoped<IAuthorizationHandler, MemberRequirementNonRevokedHandler>();
builder.Services.AddScoped<IAuthorizationHandler, MemberRequirementSystemAdminHandler>();

#endregion

#region Auth

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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
    .AddDefaultPolicy(Policies.Member, policy =>
        policy.AddRequirements(new MemberRequirement())
    );

#endregion

#region FastEndpoints

builder.Services.AddFastEndpoints()
    .SwaggerDocument(options =>
    {
        options.DocumentSettings = s =>
        {
            s.Title = "SST Alumni Association Member API";
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
        policy.WithHeaders("Authorization", "Content-Type");
        policy.WithOrigins(
            "https://app.sstaa.org",
            "http://localhost:3000"
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
        c.OutputPath = Path.Combine("..", "SSTAlumniAssociation.WebApp", "api", "member");
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

app.Run();