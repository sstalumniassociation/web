namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Create;

public class CreateEventRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string BadgeImage { get; set; }
    public bool Active { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}