using System.Security.Claims;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.Core.Extensions;

/// <summary>
/// Extensions for Principle claims
/// </summary>
public static class ClaimsExtensions
{
    /// <summary>
    /// Retrieves the name identifier (represents <see cref="User.Id"/> as modified by <see cref="Authorization.ClaimsTransformation"/>)
    /// </summary>
    /// <param name="claims"></param>
    /// <returns>ID of user</returns>
    public static string GetNameIdentifier(this IEnumerable<Claim> claims)
    {
        return claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value;
    }

    /// <summary>
    /// Retrieves the name identifier (represents <see cref="User.Id"/> as modified by <see cref="Authorization.ClaimsTransformation"/>)
    /// </summary>
    /// <param name="claims"></param>
    /// <returns>ID of user</returns>
    public static Guid GetNameIdentifierGuid(this IEnumerable<Claim> claims)
    {
        return Guid.Parse(claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}