using FluentValidation;
using Protos.CheckIn.V1;

namespace SSTAlumniAssociation.WebApi.Services.V1.CheckIn;

/// <inheritdoc />
public class CreateCheckInRequestValidator : AbstractValidator<CreateCheckInRequest>
{
    /// <inheritdoc />
    public CreateCheckInRequestValidator()
    {
        RuleFor(r => r.CheckIn).NotEmpty();

        When(r => r.CheckIn is not null, () =>
        {
            When(r => r.CheckIn.Guest is not null, () =>
            {
                RuleFor(r => r.CheckIn.Guest.Name).MinimumLength(1);
                RuleFor(r => r.CheckIn.Guest.Nric).MinimumLength(4).MaximumLength(4);
                RuleFor(r => r.CheckIn.Guest.Phone).MinimumLength(8).MaximumLength(8);
            });
        });
    }
}