namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Article.Create;

public class CreateArticleRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string HeroImageUrl { get; set; }
    public string HeroImageAlt { get; set; }
    public string CtaUrl { get; set; }
    public string CtaTitle { get; set; }
}