using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Member;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "discriminator")]
[JsonDerivedType(typeof(CreateAlumniMemberRequest), "AlumniMember")]
[JsonDerivedType(typeof(CreateEmployeeMemberRequest), "EmployeeMember")]
public class CreateMemberRequest
{
    public string Name { get; set; }
    public string PreferredName { get; set; }
    [Phone] public string Phone { get; set; }
    public string Telegram { get; set; }
    public string MailingAddress { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string SstEmail { get; set; }
}

public class CreateAlumniMemberRequest : CreateMemberRequest;

public class CreateEmployeeMemberRequest : CreateMemberRequest;