using FastEndpoints;
using FastEndpoints.Swagger;
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

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Postgres"));
dataSourceBuilder.MapEnum<ServiceAccountType>();
dataSourceBuilder.MapEnum<PaymentIntentState>();
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        if (builder.Environment.IsDevelopment())
        {
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        }

        options.UseNpgsql(dataSource,
            npgsqlOptions => { npgsqlOptions.MigrationsAssembly("SSTAlumniAssociation.Migrations"); });
    }
);

#endregion

#region Services

builder.Services.AddScoped<IAuthorizationHandler, AdminRequirementExcoHandler>();
builder.Services.AddScoped<IAuthorizationHandler, AdminRequirementSystemAdminHandler>();

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
    .AddDefaultPolicy(Policies.Admin, policy =>
        policy.AddRequirements(new AdminRequirement())
    );

#endregion

#region FastEndpoints

builder.Services.AddFastEndpoints()
    .SwaggerDocument();

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
builder.Services.SwaggerDocument(options =>
{
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

var app = builder.Build();

app.UseFastEndpoints(options => { options.Versioning.Prefix = "v"; })
    .UseSwaggerGen(options => { options.Path = "/openapi/{documentName}.json"; });

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();

    app.MapScalarApiReference();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();