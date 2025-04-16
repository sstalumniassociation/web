namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Event;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string BadgeImage { get; set; }
    public bool Active { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}