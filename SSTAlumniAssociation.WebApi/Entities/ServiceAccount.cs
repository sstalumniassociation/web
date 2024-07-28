namespace SSTAlumniAssociation.WebApi.Entities;

public class ServiceAccount : User
{
    public ServiceAccountType ServiceAccountType { get; set; }

    public ICollection<Attendee> AttendeesAdmitted { get; } = [];
    public ICollection<CheckIn> CheckIns { get; } = [];
}