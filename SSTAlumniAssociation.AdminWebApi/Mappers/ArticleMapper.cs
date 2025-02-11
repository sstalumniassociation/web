using Riok.Mapperly.Abstractions;
using SSTAlumniAssociation.AdminWebApi.Endpoints.Article;
using SSTAlumniAssociation.AdminWebApi.Endpoints.CheckIn;
using SSTAlumniAssociation.Core.Entities;

namespace SSTAlumniAssociation.AdminWebApi.Mappers;

[Mapper]
public static partial class ArticleMapper
{
    public static partial ArticleResponse ToResponse(this Article checkIn);
}