using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.AdminWebApi.Endpoints.User;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
public static partial class UserMapper
{
    public static partial UserResponse ToResponse(this User user);
}