using SSTAlumniAssociation.AdminWebApi.Endpoints.User.Create;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.BatchCreate;

public class BatchCreateUserRequest
{
    public List<CreateUserRequest> Users { get; set; }
}