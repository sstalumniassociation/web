using System.Text.Json.Serialization;

namespace SSTAlumniAssociation.Core.Dtos.User;

[
    JsonPolymorphic(
        TypeDiscriminatorPropertyName = "type",
        UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor
    ),
    JsonDerivedType(typeof(EmployeeResponse), "Employee"),
    JsonDerivedType(typeof(MemberResponse), "Member"),
    JsonDerivedType(typeof(AlumniMemberResponse), "AlumniMember"),
    JsonDerivedType(typeof(EmployeeMemberResponse), "EmployeeMember"),
    JsonDerivedType(typeof(ServiceAccountResponse), "ServiceAccount"),
    JsonDerivedType(typeof(SystemAdminResponse), "SystemAdmin")
]
public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string FirebaseId { get; set; }
    public bool Revoked { get; set; }
}