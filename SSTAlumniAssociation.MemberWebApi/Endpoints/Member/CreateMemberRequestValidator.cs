using System.Text.Json.Serialization;
using FastEndpoints;
using FluentValidation;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Member;

public class CreateMemberRequestValidator : Validator<CreateMemberRequest>
{
    public CreateMemberRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty();

        RuleFor(r => r.PreferredName)
            .NotEmpty();

        RuleFor(r => r.Phone)
            .Must(p => p.StartsWith("+65 ") && p.Skip(3).First() is '6' or '8' or '9')
            .WithMessage("Phone must be Singapore number.");

        RuleFor(r => r.MailingAddress)
            .NotEmpty();

        RuleFor(r => r.DateOfBirth)
            .Must(d => d.Year <= DateOnly.FromDateTime(DateTime.Now).Year - 12);

        RuleFor(r => r.SstEmail)
            .Must(d => d.EndsWith("sst.edu.sg"));
    }
}