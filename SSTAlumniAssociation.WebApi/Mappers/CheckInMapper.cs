using Riok.Mapperly.Abstractions;

namespace SSTAlumniAssociation.WebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class CheckInMapper
{
    #region gRPC mappings

    [MapDerivedType<Entities.GuestCheckIn, Protos.CheckIn.V1.CheckIn>]
    [MapDerivedType<Entities.UserCheckIn, Protos.CheckIn.V1.CheckIn>]
    public static partial Protos.CheckIn.V1.CheckIn ToGrpc(this Entities.CheckIn checkIn);

    public static partial Protos.CheckIn.V1.CheckIn ToGrpcBase(this Entities.CheckIn checkIn);

    // TODO : rename methods
    public static partial Protos.CheckIn.V1.GuestCheckIn ToGrpcBaseGuest(this Entities.GuestCheckIn checkIn);

    private static Protos.CheckIn.V1.CheckIn ToGrpc(Entities.GuestCheckIn checkIn)
    {
        var c = checkIn.ToGrpcBase();
        c.Guest = checkIn.ToGrpcBaseGuest();
        return c;
    }

    public static partial IEnumerable<Protos.CheckIn.V1.CheckIn> ToGrpc(this IQueryable<Entities.CheckIn> checkIn);

    #endregion

    #region Entity mappings
    
    [MapNestedProperties(nameof(Protos.CheckIn.V1.CheckInSimple.Guest))]
    public static partial Entities.GuestCheckIn ToGuestCheckIn(this Protos.CheckIn.V1.CheckInSimple checkIn);

    
    [MapperIgnoreSource(nameof(Protos.CheckIn.V1.CheckIn.User))]
    [MapProperty(nameof(Protos.CheckIn.V1.CheckInSimple.User), nameof(Entities.UserCheckIn.UserId))]
    public static partial Entities.UserCheckIn ToUserCheckIn(this Protos.CheckIn.V1.CheckInSimple checkIn);

    [ObjectFactory]
    private static Entities.GuestCheckIn CreateGuestCheckIn()
    {
        return new Entities.GuestCheckIn
        {
            CheckInDateTime = DateTime.UtcNow,
            Name = string.Empty,
            Nric = string.Empty,
            Phone = string.Empty,
            Reason = string.Empty
        };
    }

    [ObjectFactory]
    private static Entities.UserCheckIn CreateUserCheckIn()
    {
        return new Entities.UserCheckIn
        {
            CheckInDateTime = DateTime.UtcNow
        };
    }

    #endregion
}