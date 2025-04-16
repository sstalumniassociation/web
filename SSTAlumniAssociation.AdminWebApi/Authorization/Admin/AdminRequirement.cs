using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Authorization.Admin;

/// <summary>
/// User must be a <see cref="ServiceAccount"/>
/// </summary>
public class AdminRequirement : IAuthorizationRequirement;