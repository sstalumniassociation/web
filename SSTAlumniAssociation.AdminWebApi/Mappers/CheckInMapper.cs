using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.AdminWebApi.Endpoints.CheckIn;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
[UseStaticMapper(typeof(UserMapper))]
public static partial class CheckInMapper
{
    [MapDerivedType<GuestCheckIn, GuestCheckInResponse>]
    [MapDerivedType<UserCheckIn, UserCheckInResponse>]
    public static partial CheckInResponse ToResponse(this CheckIn checkIn);
    
    public static partial IEnumerable<CheckInResponse> ToResponse(this IEnumerable<CheckIn> checkIn);
}