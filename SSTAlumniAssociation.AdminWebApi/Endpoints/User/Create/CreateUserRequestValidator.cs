using FastEndpoints;
using FluentValidation;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.User.Create;

public class CreateUserRequestValidator: Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MinimumLength(5)
            .WithMessage("Name should be at least 5 characters.");
    }
}