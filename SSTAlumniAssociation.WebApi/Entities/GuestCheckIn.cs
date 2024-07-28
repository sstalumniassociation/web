namespace SSTAlumniAssociation.WebApi.Entities;

/// <summary>
/// Guest check in where the information is input by the guard house
/// </summary>
public class GuestCheckIn : CheckIn
{
    public required string Name { get; set; }
    public required string Nric { get; set; }
    public required string Phone { get; set; }
    public required string Reason { get; set; }
}