using Calzolari.Grpc.AspNetCore.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

#region Database

builder.Services.AddNpgsql<AppDbContext>(
    builder.Configuration.GetConnectionString("Postgres"),
    optionsAction: options =>
    {
        if (!builder.Environment.IsDevelopment()) return;
        
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    },
    npgsqlOptionsAction: options =>
    {
        options.MigrationsAssembly("SSTAlumniAssociation.Migrations");
    }
);

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

builder.Services.AddAuthorization();

#endregion

#region gRPC

builder.Services.AddGrpc(options => { options.EnableMessageValidation(); }).AddJsonTranscoding();
builder.Services.AddGrpcValidation();

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SST Alumni Association Service Account API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Firebase ID Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                []
            }
        }
    );

    var filePath = Path.Combine(AppContext.BaseDirectory, "SSTAlumniAssociation.ServiceAccountWebApi.xml");
    options.IncludeXmlComments(filePath);
    options.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

// Require authorization by default and opt-out for anonymous routes

app.UseHttpsRedirection();

app.Run();
