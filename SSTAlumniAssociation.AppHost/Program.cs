using System.Runtime.InteropServices;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddContainer("postgres", "postgres")
    .WithVolume("postgres", "/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "sa");

postgres = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    ? postgres.WithContainerRuntimeArgs("--net=host")
    : postgres.WithEndpoint(5432, 5432);

var pgAdmin = builder.AddContainer("adminer", "adminer");

pgAdmin = RuntimeInformation.IsOSPlatform(OSPlatform.Linux)
    ? pgAdmin.WithContainerRuntimeArgs("--net=host")
    : pgAdmin.WithEndpoint(8080, 8080);

var memberWebApi = builder.AddProject<SSTAlumniAssociation_MemberWebApi>("member-web-api");

builder.AddNpmApp("webapp", "../SSTAlumniAssociation.WebApp", "dev")
    .WithReference(memberWebApi);

builder.Build().Run();