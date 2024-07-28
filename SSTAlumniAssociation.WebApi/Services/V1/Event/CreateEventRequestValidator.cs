using FluentValidation;
using Protos.Event.V1;
using Protos.User.V1;

namespace SSTAlumniAssociation.WebApi.Services.V1.Event;

/// <inheritdoc />
public class CreateEventRequestValidator : AbstractValidator<CreateEventRequest>
{
    /// <inheritdoc />
    public CreateEventRequestValidator()
    {
        RuleFor(e => e.Event).NotEmpty();

        When(e => e.Event is not null, () =>
        {
            RuleFor(e => e.Event.Name).MinimumLength(1);
            RuleFor(e => e.Event.BadgeImage)
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _));
            RuleFor(e => e.Event.StartDateTime)
                .Must((req, date) => date.ToDateTime() < req.Event.EndDateTime.ToDateTime());
        });
    }
}