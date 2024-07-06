using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.WebApi.Authorization;

/// <summary>
/// Syntax sugar for [Authorize(Policy = Policies.Admin)]
/// </summary>
public class AuthorizeAdminAttribute : AuthorizeAttribute
{
    /// <inheritdoc />
    public AuthorizeAdminAttribute()
    {
        Policy = Policies.Admin;
    }
}