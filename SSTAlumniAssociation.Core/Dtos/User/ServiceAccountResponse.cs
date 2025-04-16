namespace SSTAlumniAssociation.Core.Dtos.User;

public class ServiceAccountResponse : UserResponse
{
    public ServiceAccountTypeResponse ServiceAccountType { get; set; }
}