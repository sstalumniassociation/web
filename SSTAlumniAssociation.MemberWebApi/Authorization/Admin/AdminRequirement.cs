using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.Admin;

/// <summary>
/// User must be a <see cref="ServiceAccount"/>
/// </summary>
public class AdminRequirement : IAuthorizationRequirement;