using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddContainer("postgres", "postgres")
    .WithVolume("postgres", "/var/lib/postgresql/data")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "sa")
    .WithEndpoint(5432, 5432);

var pgAdmin = builder.AddContainer("pgadmin", "dpage/pgadmin4")
    .WithVolume("pgadmin", "/var/lib/pgadmin")
    .WithEnvironment("PGADMIN_DEFAULT_EMAIL", "pgadmin4@pgadmin.org")
    .WithEnvironment("PGADMIN_CONFIG_SERVER_MODE", "False")
    .WithEnvironment("PGADMIN_DEFAULT_PASSWORD", "admin")
    .WithEndpoint(5050, 80);

var api = builder.AddProject<SSTAlumniAssociation_WebApi>("web-api");

builder.AddNpmApp("web-app", "../SSTAlumniAssociation.WebApp", "dev")
    .WithReference(api);

builder.Build().Run();
