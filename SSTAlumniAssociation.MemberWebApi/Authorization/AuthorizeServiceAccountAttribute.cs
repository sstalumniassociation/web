using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Authorization;

/// <summary>
/// Syntax sugar for [Authorize(Policy = Policies.ServiceAccount)]
/// </summary>
public class AuthorizeServiceAccountAttribute: AuthorizeAttribute
{
    /// <inheritdoc />
    public AuthorizeServiceAccountAttribute()
    {
        Policy = Policies.ServiceAccount;
    }
}