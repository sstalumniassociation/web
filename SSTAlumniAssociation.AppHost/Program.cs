var builder = DistributedApplication.CreateBuilder(args);

builder.AddNpmApp("web-app", "../SSTAlumniAssociation.WebApp", "dev");

builder.Build().Run();