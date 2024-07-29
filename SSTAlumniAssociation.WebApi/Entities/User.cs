using Microsoft.EntityFrameworkCore;

namespace SSTAlumniAssociation.WebApi.Entities;

/// <summary>
/// Base user class
/// </summary>
[Index(nameof(FirebaseId), IsUnique = true)]
public class User
{
    public Guid Id { get; set; }

    /// <summary>
    /// Ignore Firebase Auth provided values and force user to provide their own.
    /// </summary>
    public required string Name { get; set; }

    public required string Email { get; set; }

    /// <summary>
    /// Use Firebase Auth provided ID as SSOT.
    /// </summary>
    public required string FirebaseId { get; set; }

    // Navigations
    
    public ICollection<Event> Events { get; } = [];
    public ICollection<Attendee> UserEvents { get; } = [];
    public ICollection<UserCheckIn> CheckIns { get; } = [];
}