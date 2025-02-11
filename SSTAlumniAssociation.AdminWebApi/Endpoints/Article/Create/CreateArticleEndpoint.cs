using FastEndpoints;
using SSTAlumniAssociation.AdminWebApi.Mappers;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Article.Create;

public class CreateArticleEndpoint(AppDbContext dbContext) : Endpoint<CreateArticleRequest, ArticleResponse>
{
    public override void Configure()
    {
        Post("/Article");
        Policies(Authorization.Policies.Admin);
    }

    public override async Task HandleAsync(CreateArticleRequest req, CancellationToken ct)
    {
        var article = await dbContext.Articles.AddAsync(new Core.Entities.Article
        {
            Title = req.Title,
            Description = req.Description,
            HeroImageUrl = req.HeroImageUrl,
            HeroImageAlt = req.HeroImageAlt,
            CtaUrl = req.HeroImageUrl,
            CtaTitle = req.CtaTitle
        }, ct);

        await dbContext.SaveChangesAsync(ct);

        await SendAsync(article.Entity.ToResponse(), cancellation: ct);
    }
}