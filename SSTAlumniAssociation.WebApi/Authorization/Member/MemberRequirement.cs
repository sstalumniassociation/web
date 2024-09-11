using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.WebApi.Authorization.Member;

/// <summary>
/// User must be a <see cref="Member"/> with a member type that is not <see cref="Membership.Revoked"/> .
/// </summary>
public class MemberRequirement : IAuthorizationRequirement;