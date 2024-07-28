using Calzolari.Grpc.AspNetCore.Validation;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SSTAlumniAssociation.ServiceDefaults;
using SSTAlumniAssociation.WebApi.Authorization;
using SSTAlumniAssociation.WebApi.Authorization.Admin;
using SSTAlumniAssociation.WebApi.Authorization.Member;
using SSTAlumniAssociation.WebApi.Authorization.OwnerOrAdmin;
using SSTAlumniAssociation.WebApi.Context;
using SSTAlumniAssociation.WebApi.Services.V1;
using SSTAlumniAssociation.WebApi.Services.V1.User;

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
    }
);

#endregion

#region Services

builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformation>();
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
builder.Services.AddGrpcValidation();

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowCredentials();
        policy.WithHeaders("Authorization");
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
                Array.Empty<string>()
            }
        }
    );

    var filePath = Path.Combine(AppContext.BaseDirectory, "SSTAlumniAssociation.WebApi.xml");
    options.IncludeXmlComments(filePath);
    options.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

builder.Services.AddGrpcSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await using var scope = app.Services.CreateAsyncScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.EnsureDeletedAsync();
    await db.Database.EnsureCreatedAsync();
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

app.UseHttpsRedirection();

app.Run();