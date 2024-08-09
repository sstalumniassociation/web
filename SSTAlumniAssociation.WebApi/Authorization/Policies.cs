namespace SSTAlumniAssociation.WebApi.Authorization;

/// <summary>
/// Policy names
/// </summary>
public class Policies
{
    /// <summary>
    /// Admins
    /// </summary>
    public const string Admin = "Admin";

    /// <summary>
    /// Members
    /// </summary>
    public const string Member = "Member";

    /// <summary>
    /// Resource owner or admins
    /// </summary>
    public const string OwnerOrAdmin = "OwnerOrAdmin";

    /// <summary>
    /// Service accounts
    /// </summary>
    public const string ServiceAccount = "ServiceAccount";
}