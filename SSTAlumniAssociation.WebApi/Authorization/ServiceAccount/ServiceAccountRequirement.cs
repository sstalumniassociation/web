using Microsoft.AspNetCore.Authorization;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.WebApi.Authorization.ServiceAccount;

/// <summary>
/// User must be a <see cref="ServiceAccount"/>
/// </summary>
public class ServiceAccountRequirement : IAuthorizationRequirement;