using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Authorization;

/// <summary>
/// Syntax sugar for [Authorize(Policy = Policies.Member)]
/// </summary>
public class AuthorizeMemberAttribute : AuthorizeAttribute
{
    /// <inheritdoc />
    public AuthorizeMemberAttribute()
    {
        Policy = Policies.Member;
    }
}