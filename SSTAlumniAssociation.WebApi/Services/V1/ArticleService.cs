using Protos.Article.V1;
using SSTAlumniAssociation.WebApi.Authorization;

namespace SSTAlumniAssociation.WebApi.Services.V1;

[AuthorizeMember]
public class ArticleServiceV1 : ArticleService.ArticleServiceBase
{
}