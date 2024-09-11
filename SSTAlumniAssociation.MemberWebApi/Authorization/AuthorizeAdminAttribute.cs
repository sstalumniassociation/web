using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Authorization;

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