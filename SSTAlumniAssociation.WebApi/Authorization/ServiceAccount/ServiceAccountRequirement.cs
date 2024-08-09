using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.WebApi.Authorization.ServiceAccount;

/// <summary>
/// User must be a <see cref="Entities.ServiceAccount"/>
/// </summary>
public class ServiceAccountRequirement : IAuthorizationRequirement;