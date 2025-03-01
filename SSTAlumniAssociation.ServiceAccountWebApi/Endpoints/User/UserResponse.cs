using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.User;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string FirebaseId { get; set; }
    public bool Revoked { get; set; }
}

public class EmployeeResponse : UserResponse;

public abstract class MemberResponse : UserResponse
{
    public string MemberId { get; set; }
}

public class AlumniMemberResponse : MemberResponse
{
    public int? GraduationYear { get; set; }
}

public class EmployeeMemberResponse : MemberResponse
{
    public int? GraduationYear { get; set; }
}

public class ServiceAccountResponse : UserResponse
{
    public ServiceAccountType ServiceAccountType { get; set; }
}

public class SystemAdminResponse : UserResponse;