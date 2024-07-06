namespace SSTAlumniAssociation.WebApi.Entities;

public class Event
{
    public Guid Id { get; set; }

    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required string BadgeImage { get; set; }
    public required DateTime StartDateTime { get; set; }
    public required DateTime EndDateTime { get; set; }

    // Navigations 
    
    public List<User> Attendees { get; set; }
    public List<UserEvent> UserEvents { get; set; }
}
