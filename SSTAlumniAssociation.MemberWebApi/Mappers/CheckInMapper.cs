using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(GrpcMapper))]
[UseStaticMapper(typeof(UserMapper))]
public static partial class CheckInMapper
{
    #region gRPC mappings

    [MapDerivedType<GuestCheckIn, Protos.CheckIn.V1.CheckIn>]
    [MapDerivedType<UserCheckIn, Protos.CheckIn.V1.CheckIn>]
    public static partial Protos.CheckIn.V1.CheckIn ToGrpc(this CheckIn checkIn);

    public static partial Protos.CheckIn.V1.CheckIn ToGrpcBase(this CheckIn checkIn);

    // TODO : rename methods
    public static partial Protos.CheckIn.V1.GuestCheckIn ToGrpcBaseGuest(this GuestCheckIn checkIn);

    private static Protos.CheckIn.V1.CheckIn ToGrpc(GuestCheckIn checkIn)
    {
        var c = checkIn.ToGrpcBase();
        c.Guest = checkIn.ToGrpcBaseGuest();
        return c;
    }

    public static partial IEnumerable<Protos.CheckIn.V1.CheckIn> ToGrpc(this IQueryable<CheckIn> checkIn);

    #endregion

    #region Entity mappings
    
    [MapNestedProperties(nameof(Protos.CheckIn.V1.CheckInSimple.Guest))]
    public static partial GuestCheckIn ToGuestCheckIn(this Protos.CheckIn.V1.CheckInSimple checkIn);

    
    [MapperIgnoreSource(nameof(Protos.CheckIn.V1.CheckIn.User))]
    [MapProperty(nameof(Protos.CheckIn.V1.CheckInSimple.User), nameof(UserCheckIn.UserId))]
    public static partial UserCheckIn ToUserCheckIn(this Protos.CheckIn.V1.CheckInSimple checkIn);

    [ObjectFactory]
    private static GuestCheckIn CreateGuestCheckIn()
    {
        return new GuestCheckIn
        {
            CheckInDateTime = DateTime.UtcNow,
            Name = string.Empty,
            Nric = string.Empty,
            Phone = string.Empty,
            Reason = string.Empty
        };
    }

    [ObjectFactory]
    private static UserCheckIn CreateUserCheckIn()
    {
        return new UserCheckIn
        {
            CheckInDateTime = DateTime.UtcNow
        };
    }

    #endregion
}