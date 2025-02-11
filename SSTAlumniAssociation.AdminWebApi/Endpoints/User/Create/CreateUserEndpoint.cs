using FastEndpoints;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Event;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.Create;

public class CreateUserEndpoint(AppDbContext dbContext) : Endpoint<CreateUserRequest, EventResponse>
{
    public override void Configure()
    {
        Post("/User");
        // Policies(Authorization.Policies.Admin);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        switch (req)
        {
            case CreateSystemAdminRequest r:
            {
                Console.WriteLine("r");
                break;
            }
            case CreateServiceAccountRequest rr:
            {
                Console.WriteLine("rr");
                break;
            }
        }
    }
}