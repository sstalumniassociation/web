namespace SSTAlumniAssociation.WebApi.Entities;

public class ServiceAccount : User
{
    public ServiceAccountType ServiceAccountType { get; set; }
}