using Protos.Article.V1;
using SSTAlumniAssociation.MemberWebApi.Authorization;

namespace SSTAlumniAssociation.MemberWebApi.Services.V1;

[AuthorizeMember]
public class ArticleServiceV1 : ArticleService.ArticleServiceBase
{
}