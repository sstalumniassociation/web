using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.WebApi.Authorization.Admin;

/// <summary>
/// Users must be either a <see cref="Entities.SystemAdmin"/> or <see cref="Entities.Member"/> with an <see cref="Membership.Exco"/> member type.
/// Users which fulfill this requirement will be able to access admin capabilities, which includes reading PII and modifying data.
///
/// Please grant with caution.
/// </summary>
public class AdminRequirement : IAuthorizationRequirement;