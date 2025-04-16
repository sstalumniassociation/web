using Microsoft.EntityFrameworkCore;

namespace SSTAlumniAssociation.Core.Entities;

public class ManualMemberApproval
{
    public Guid Id { get; set; }

    public required DateTime CreatedAt { get; set; }
    public required string Reason { get; set; }

    #region Navigations

    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    
    /// <summary>
    /// EXCO or <see cref="SystemAdmin"/>
    /// </summary>
    public Guid ApproverId { get; set; }

    /// <summary>
    /// EXCO or <see cref="SystemAdmin"/>
    /// </summary>
    public User Approver { get; set; }

    #endregion
}