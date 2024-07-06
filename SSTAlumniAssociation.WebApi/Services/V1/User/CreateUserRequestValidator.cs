using FluentValidation;
using Protos.User.V1;

namespace SSTAlumniAssociation.WebApi.Services.V1.User;

/// <inheritdoc />
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    /// <inheritdoc />
    public CreateUserRequestValidator()
    {
        RuleFor(u => u.User).NotEmpty();

        When(u => u.User != null, () =>
        {
            RuleFor(u => u.User.Id)
                .Must(id => Guid.TryParse(id, out _))
                .WithMessage("Invalid ID provided for User.");
            RuleFor(u => u.User.Name).MinimumLength(1);
            RuleFor(u => u.User.Email).EmailAddress();

            When(u => u.User.Member != null, () =>
            {
                RuleFor(u => u.User.Member.AlumniMember)
                    .Must(u => u.HasGraduationYear && u.GraduationYear >= 2013)
                    .WithMessage("Graduation year must be after 2013.")
                    .When(u => u.User.Member.AlumniMember != null);

                RuleFor(u => u.User.Member.EmployeeMember)
                    .Must(u => u.HasGraduationYear && u.GraduationYear >= 2013)
                    .WithMessage("Graduation year must be after 2013.")
                    .When(u => u.User.Member.EmployeeMember != null);

                // In the case of an associate member, they may have studied in SST but not graduated.
                RuleFor(u => u.User.Member)
                    .Must(u => !u.AlumniMember.HasGraduationYear && u.Membership == Membership.Associate)
                    .WithMessage("Alumni membership can only be associate if there is no graduation year.");
            });
        });
    }
}