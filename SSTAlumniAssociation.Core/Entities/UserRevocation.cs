using System.ComponentModel.DataAnnotations;

namespace SSTAlumniAssociation.Core.Entities;

/// <summary>
/// A revoked user means that they will not be able to access the school campus
/// </summary>
public class UserRevocation
{
    public Guid Id { get; set; }

    /// <summary>
    /// Reason for revocation
    /// </summary>
    public required string Reason { get; set; }

    public required DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    #region Navigations

    public Guid UserId { get; set; }
    public User User { get; set; }

    #endregion
}