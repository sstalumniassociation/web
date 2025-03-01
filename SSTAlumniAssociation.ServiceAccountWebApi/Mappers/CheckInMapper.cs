using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.ServiceAccountWebApi.Endpoints.CheckIn;

namespace SSTAlumniAssociation.ServiceAccountWebApi.Mappers;

[Mapper]
public static partial class CheckInMapper
{
    [MapDerivedType<GuestCheckIn, GuestCheckInResponse>]
    [MapDerivedType<UserCheckIn, UserCheckInResponse>]
    public static partial CheckInResponse ToResponse(this CheckIn checkIn);
}