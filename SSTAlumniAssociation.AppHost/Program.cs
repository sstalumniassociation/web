using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgWeb();

var memberWebApi = builder.AddProject<SSTAlumniAssociation_MemberWebApi>("member-web-api")
    .WithReference(postgres);
var adminWebApi = builder.AddProject<SSTAlumniAssociation_AdminWebApi>("admin-web-api")
    .WithReference(postgres);
var serviceAccountWebApi = builder.AddProject<SSTAlumniAssociation_ServiceAccountWebApi>("service-account-web-api")
    .WithReference(postgres);

builder.AddNpmApp("webapp", "../SSTAlumniAssociation.WebApp", "dev")
    .WithReference(memberWebApi)
    .WithReference(adminWebApi)
    .WithReference(serviceAccountWebApi);

builder.Build().Run();