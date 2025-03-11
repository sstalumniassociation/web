using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.Core.Extensions;

/// <summary>
/// Extensions for Principle claims
/// </summary>
public static class ClaimsExtensions
{
    public static string? GetEmail(this IEnumerable<Claim> claims)
    {
        return claims.FirstOrDefault(c => c.Type == "email")?.Value;
    }
}