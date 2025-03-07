using FastEndpoints;
using FluentValidation;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Article.Create;

public class CreateArticleRequestValidator : Validator<CreateArticleRequest>
{
    public CreateArticleRequestValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MinimumLength(5)
            .WithMessage("Title should be at least 5 characters.");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(5)
            .WithMessage("Description should be at least 5 characters.");

        RuleFor(r => r.HeroImageAlt)
            .NotEmpty()
            .WithMessage("Image alt is required.")
            .MinimumLength(5)
            .WithMessage("Image alt should be at least 5 characters.");

        RuleFor(r => r.CtaTitle)
            .NotEmpty()
            .WithMessage("Call to action title is required.")
            .MinimumLength(5)
            .WithMessage("Call to action title should be at least 5 characters.");
    }
}