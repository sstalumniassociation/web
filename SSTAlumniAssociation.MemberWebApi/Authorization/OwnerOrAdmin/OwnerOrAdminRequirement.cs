using Microsoft.AspNetCore.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Authorization.OwnerOrAdmin;

/// <summary>
/// For the operations below:
/// <list type="bullet">
/// 
/// <item><description>
/// <see cref="OwnerOrAdminOperations.Create"/>: the user must pass the <see cref="Admin.AdminRequirement"/>.
/// </description></item>
/// 
/// <item><description>
/// <see cref="OwnerOrAdminOperations.Read"/>: the user is able to view their own data, but must pass the
/// <see cref="Admin.AdminRequirement"/> in order to view data of other users.
/// </description></item>
/// 
/// <item><description>
/// <see cref="OwnerOrAdminOperations.Update"/>: the user must pass the <see cref="Admin.AdminRequirement"/>. This is
/// because SSTAA does not allow self-service user profile updates at the moment, although it might be allowed in the future.
/// </description></item>
/// 
/// <item><description>
/// <see cref="OwnerOrAdminOperations.Delete"/>: the user must pass the <see cref="Admin.AdminRequirement"/>.
/// </description></item>
/// 
/// </list>
/// </summary>
public class OwnerOrAdminRequirement : IAuthorizationRequirement
{
    public string Name { get; set; }
};