using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.Core.Extensions;

/// <summary>
/// Extensions for Principle claims
/// </summary>
public static class ClaimsExtensions
{
    public static IQueryable<T> WhereUserMatchesEmailFromClaims<T>(this DbSet<T> dbSet, IEnumerable<Claim> claims) where T : User
    {
        var email = claims.SingleOrDefault(c => c.Type == "email");
        ArgumentNullException.ThrowIfNull(email);
        return dbSet.Where(u => u.Email == email.Value);
    }

    public static IQueryable<T> WhereUserMatchesEmailFromClaims<T, T2>(
        this IIncludableQueryable<T, T2> dbSet,
        IEnumerable<Claim> claims
    ) where T : User
    {
        var email = claims.SingleOrDefault(c => c.Type == "email");
        ArgumentNullException.ThrowIfNull(email);
        return dbSet.Where(u => u.Email == email.Value);
    }
}