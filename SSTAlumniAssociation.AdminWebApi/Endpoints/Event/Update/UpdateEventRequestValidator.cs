using FastEndpoints;
using FluentValidation;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Update;

public class UpdateEventRequestValidator : Validator<UpdateEventRequest>
{
    public UpdateEventRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(5)
            .WithMessage("Name should be at least 5 characters.");

        RuleFor(r => r.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MinimumLength(5)
            .WithMessage("Description should be at least 5 characters.");

        RuleFor(r => r.Location)
            .NotEmpty()
            .WithMessage("Location is required.")
            .MinimumLength(5)
            .WithMessage("Location should be at least 5 characters.");

        RuleFor(r => r.BadgeImage)
            .NotEmpty()
            .WithMessage("Badge image is required.")
            .MinimumLength(5)
            .WithMessage("Badge image should be at least 5 characters.");

        RuleFor(r => r.StartDateTime)
            .Must((r, _) => r.StartDateTime < r.EndDateTime)
            .WithMessage("End date time must be after start date time.");
    }
}