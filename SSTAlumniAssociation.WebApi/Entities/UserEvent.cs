namespace SSTAlumniAssociation.WebApi.Entities;

public class UserEvent
{
    public Guid Id { get; set; }

    /// <summary>
    /// The admission key is the same as the ID generated
    /// </summary>
    public Guid AdmissionKey => Id;
    
    // Navigations

    public Guid UserId { get; set; }
    public required User User { get; set; }
    public Guid EventId { get; set; }
    public required Event Event { get; set; }
}