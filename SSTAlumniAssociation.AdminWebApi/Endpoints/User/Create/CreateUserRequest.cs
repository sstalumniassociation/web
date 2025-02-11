using System.Text.Json.Serialization;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.Create;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
[JsonDerivedType(typeof(CreateEmployeeMemberRequest), "EmployeeMember")]
[JsonDerivedType(typeof(CreateAlumniMemberRequest), "AlumniMember")]
[JsonDerivedType(typeof(CreateEmployeeRequest), "Employee")]
[JsonDerivedType(typeof(CreateSystemAdminRequest), "SystemAdmin")]
[JsonDerivedType(typeof(CreateServiceAccountRequest), "ServiceAccount")]
public abstract class CreateUserRequest
{
    /// <summary>
    /// This accepts
    /// - EmployeeMember
    /// - AlumniMember
    /// - Employee
    /// - SystemAdmin
    /// - ServiceAccount
    /// </summary>
    [JsonPropertyName("$type")]
    public string Type { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public string FirebaseId { get; set; }
}

public abstract class CreateMemberRequest : CreateUserRequest
{
    public string MemberId { get; set; }
}

public class CreateAlumniMemberRequest : CreateMemberRequest
{
    public int? GraduationYear { get; set; }
}

public class CreateEmployeeMemberRequest : CreateMemberRequest
{
    public int? GraduationYear { get; set; }
}

public class CreateEmployeeRequest : CreateUserRequest;

public class CreateSystemAdminRequest : CreateUserRequest;

public class CreateServiceAccountRequest : CreateUserRequest
{
    public ServiceAccountType ServiceAccountType { get; set; }
}