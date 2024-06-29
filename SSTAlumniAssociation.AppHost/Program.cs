using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<SSTAlumniAssociation_WebApi>("web-api");
builder.AddNpmApp("web-app", "../SSTAlumniAssociation.WebApp", "dev")
    .WithReference(api);

builder.Build().Run();