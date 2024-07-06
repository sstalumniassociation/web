namespace SSTAlumniAssociation.WebApi.Entities;

/// <summary>
/// The role that the member has within SSTAA.
/// This value is stored in the database as a string.
/// This enum should rarely ever be modified, as it will very likely cause a breaking change.
/// </summary>
public enum Membership
{
    Exco,

    /// <summary>
    /// All past/present staff and students who completed at least 1 year of study in SST but did not graduate
    /// </summary>
    Associate,

    /// <summary>
    /// All graduated alumni who are under 21
    /// </summary>
    Affiliate,
    Ordinary,
    Revoked,
}
