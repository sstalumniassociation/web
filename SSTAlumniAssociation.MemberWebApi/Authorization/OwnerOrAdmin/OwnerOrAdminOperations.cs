namespace SSTAlumniAssociation.MemberWebApi.Authorization.OwnerOrAdmin;

/// <summary>
/// Operations on resources from either admins or the resource owner
/// </summary>
public static class OwnerOrAdminOperations
{
    /// <summary>
    /// Create resource
    /// </summary>
    public static OwnerOrAdminRequirement Create = new() { Name = nameof(Create) };
    /// <summary>
    /// Read resource
    /// </summary>
    public static OwnerOrAdminRequirement Read = new() { Name = nameof(Read) };
    /// <summary>
    /// Update resource
    /// </summary>
    public static OwnerOrAdminRequirement Update = new() { Name = nameof(Update) };
    /// <summary>
    /// Delete resource
    /// </summary>
    public static OwnerOrAdminRequirement Delete = new() { Name = nameof(Delete) };
}