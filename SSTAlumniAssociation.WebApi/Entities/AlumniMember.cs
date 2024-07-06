namespace SSTAlumniAssociation.WebApi.Entities;

/// <summary>
/// If the member was previously a student of SST, they are considered an alumni member.
/// </summary>
public class AlumniMember : Member
{
    /// <summary>
    /// In the case of an associate member, they may have studied in SST but not graduated.
    /// In this case, the membership granted should be limited to <see cref="Membership.Associate"/>
    /// </summary>
    public int? GraduationYear { get; set; }
}