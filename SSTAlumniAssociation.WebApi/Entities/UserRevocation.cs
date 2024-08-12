using System.ComponentModel.DataAnnotations;

namespace SSTAlumniAssociation.WebApi.Entities;

/// <summary>
/// A revoked user means that they will not be able to access the school campus
/// </summary>
public class UserRevocation
{
    /// <summary>
    /// Reason for revocation
    /// </summary>
    public required string Reason { get; set; }
    
    #region Navigations
    
    [Key] public Guid UserId { get; set; }
    public User User { get; set; }
    
    #endregion
}