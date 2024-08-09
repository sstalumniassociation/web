using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.WebApi.Authorization;

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