using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.Core.Entities;
using SSTAlumniAssociation.MemberWebApi.Endpoints.CheckIn;

namespace SSTAlumniAssociation.MemberWebApi.Mappers;

[Mapper]
public static partial class CheckInMapper
{
    [MapDerivedType<UserCheckIn, CheckInResponse>]
    public static partial CheckInResponse ToResponse(this CheckIn checkIn);
}