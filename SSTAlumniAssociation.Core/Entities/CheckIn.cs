namespace SSTAlumniAssociation.Core.Entities;

public abstract class CheckIn
{
    public Guid Id { get; set; }

    public required DateTime CheckInDateTime { get; set; }

    public DateTime? CheckOutDateTime { get; set; }

    #region Navigations

    public Guid ServiceAccountId { get; set; }
    public ServiceAccount ServiceAccount { get; set; }

    #endregion
}