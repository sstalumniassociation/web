using Calzolari.Grpc.AspNetCore.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceDefaults;
using SSTAlumniAssociation.MemberWebApi.Authorization;
using SSTAlumniAssociation.MemberWebApi.Authorization.Member;
using SSTAlumniAssociation.MemberWebApi.Services.V1;
using SSTAlumniAssociation.MemberWebApi.Services.V1.Event;
using SSTAlumniAssociation.MemberWebApi.Services.V1.User;

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

#region gRPC

builder.Services.AddGrpc(options => { options.EnableMessageValidation(); }).AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
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
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SST Alumni Association Member API", Version = "v1" });

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

    var filePath = Path.Combine(AppContext.BaseDirectory, "SSTAlumniAssociation.MemberWebApi.xml");
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

app.MapGrpcService<AuthService>().RequireAuthorization();
app.MapGrpcService<EventService>().RequireAuthorization();
app.MapGrpcService<UserService>().RequireAuthorization();
app.MapGrpcService<CheckInService>().RequireAuthorization();

app.UseHttpsRedirection();

app.Run();