using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Article.List;

public class ListArticleEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<IEnumerable<ArticleResponse>>
{
    public override void Configure()
    {
        Get("/Article");
        Policies(Authorization.Policies.Member);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var articles = await dbContext.Articles.ToListAsync(cancellationToken: ct);

        await SendAsync(
            articles.Select(a => new ArticleResponse
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                HeroImageUrl = a.HeroImageUrl,
                HeroImageAlt = a.HeroImageAlt,
                CtaUrl = a.HeroImageUrl,
                CtaTitle = a.CtaTitle
            }),
            cancellation: ct
        );
    }
}