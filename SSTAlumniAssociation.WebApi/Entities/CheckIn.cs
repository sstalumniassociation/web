namespace SSTAlumniAssociation.WebApi.Entities;

public abstract class CheckIn
{
    public Guid Id { get; set; }

    public DateTime CheckInDateTime { get; set; }

    public Guid CheckOutCode { get; set; }
    public DateTime? CheckOutDateTime { get; set; }

    #region Navigations

    public ServiceAccount ServiceAccount { get; set; }

    #endregion
}