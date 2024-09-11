using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.Admin;

/// <summary>
/// Users must be either a <see cref="SystemAdmin"/> or <see cref="Member"/> with an <see cref="Membership.Exco"/> member type.
/// Users which fulfill this requirement will be able to access admin capabilities, which includes reading PII and modifying data.
///
/// Please grant with caution.
/// </summary>
public class AdminRequirement : IAuthorizationRequirement;