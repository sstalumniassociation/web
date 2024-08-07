using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddContainer("postgres", "postgres")
    .WithVolume("postgres", "/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "sa")
    .WithEndpoint(5432, 5432);

var pgAdmin = builder.AddContainer("adminer", "adminer")
    .WithEndpoint(8080, 8080);

var api = builder.AddProject<SSTAlumniAssociation_WebApi>("webapi");

builder.AddNpmApp("webapp", "../SSTAlumniAssociation.WebApp", "dev")
    .WithReference(api);

builder.Build().Run();