using Calzolari.Grpc.AspNetCore.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.ServiceDefaults;
using SSTAlumniAssociation.MemberWebApi.Authorization;
using SSTAlumniAssociation.MemberWebApi.Authorization.Admin;
using SSTAlumniAssociation.MemberWebApi.Authorization.Member;
using SSTAlumniAssociation.MemberWebApi.Authorization.OwnerOrAdmin;
using SSTAlumniAssociation.MemberWebApi.Services.V1;
using SSTAlumniAssociation.MemberWebApi.Services.V1.CheckIn;
using SSTAlumniAssociation.MemberWebApi.Services.V1.Event;
using SSTAlumniAssociation.MemberWebApi.Services.V1.User;

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

#region Services

builder.Services.AddSingleton<IAuthorizationHandler, AdminSystemAdminHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminExcoHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MemberNonRevokedHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, OwnerOrAdminHandler>();

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
    .AddPolicy(Policies.Admin, policy =>
        policy.AddRequirements(new AdminRequirement()))
    .AddPolicy(Policies.Member, policy =>
        policy.AddRequirements(new MemberRequirement()))
    .AddPolicy(Policies.OwnerOrAdmin, policy =>
        policy.AddRequirements(new OwnerOrAdminRequirement()));

#endregion

#region gRPC

builder.Services.AddGrpc(options => { options.EnableMessageValidation(); }).AddJsonTranscoding();

builder.Services.AddValidator<CreateUserRequestValidator>();
builder.Services.AddValidator<CreateEventRequestValidator>();
builder.Services.AddValidator<CreateCheckInRequestValidator>();
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
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "SST Alumni Association API", Version = "v1" });

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

builder.Services.AddGrpcSwagger();

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
app.MapGrpcService<ArticleServiceV1>();
app.MapGrpcService<UserServiceV1>();
app.MapGrpcService<EventServiceV1>();
app.MapGrpcService<AuthServiceV1>();
app.MapGrpcService<AttendeeServiceV1>();
app.MapGrpcService<CheckInServiceV1>();

app.UseHttpsRedirection();

app.Run();