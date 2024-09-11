namespace SSTAlumniAssociation.Core.Entities;

public class Attendee
{
    public Guid Id { get; set; }

    /// <summary>
    /// The admission key is the same as the ID generated
    /// </summary>
    public Guid AdmissionKey => Id;

    public DateTime? AdmittedAt { get; set; }

    // Navigations

    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid EventId { get; set; }
    public Event Event { get; set; }
    
    public ServiceAccount? AdmittedBy { get; set; }
}