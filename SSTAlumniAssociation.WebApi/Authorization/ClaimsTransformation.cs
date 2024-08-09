using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.WebApi.Context;
using SSTAlumniAssociation.WebApi.Entities;

namespace SSTAlumniAssociation.WebApi.Authorization;

/// <inheritdoc />
public class ClaimsTransformation(AppDbContext dbContext) : IClaimsTransformation
{
    /// <inheritdoc />
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var sub = principal.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (sub is null)
        {
            return principal;
        }

        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.FirebaseId == sub.Value);
        if (user is null)
        {
            return principal;
        }

        if (principal.Identity is not ClaimsIdentity identity)
        {
            return principal;
        }

        var existingRoleClaim = identity.FindFirst(ClaimTypes.Role);
        if (existingRoleClaim is not null)
        {
            identity.RemoveClaim(existingRoleClaim);
        }

        switch (user)
        {
            case Entities.Member m:
                identity.AddClaim(new Claim(ClaimTypes.Role, m.Membership.ToString()));
                break;
            case Entities.ServiceAccount:
                identity.AddClaim(new Claim(ClaimTypes.Role, nameof(ServiceAccount)));
                break;
            case Employee:
                identity.AddClaim(new Claim(ClaimTypes.Role, nameof(Employee)));
                break;
            case SystemAdmin:
                identity.AddClaim(new Claim(ClaimTypes.Role, nameof(SystemAdmin)));
                break;
            default:
                throw new Exception($"Default case invoked for {nameof(user)}. Did you forget to add a new user type?");
        }

        var existingNameIdentifierClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
        if (existingNameIdentifierClaim is not null)
        {
            identity.RemoveClaim(existingNameIdentifierClaim);
        }

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

        return principal;
    }
}