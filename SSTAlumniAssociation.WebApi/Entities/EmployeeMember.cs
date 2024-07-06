namespace SSTAlumniAssociation.WebApi.Entities;

/// <summary>
/// If the member is a current staff, or ex-staff of SST, they are considered a staff member.
/// This differs from <see cref="Employee"/> as any user with EmployeeMember can be assumed to be a registered SSTAA member.
/// <see cref="Employee"/> can also only be used for current staff.
///
/// This member can only have membership of <see cref="Membership.Associate"/>.
/// </summary>
public class EmployeeMember : Member
{
    public int? GraduationYear { get; set; }
};