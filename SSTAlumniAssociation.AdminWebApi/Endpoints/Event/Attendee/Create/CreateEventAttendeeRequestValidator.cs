using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee.Create;

public class CreateEventAttendeeRequestValidator : Validator<CreateEventAttendeeRequest>
{
    public CreateEventAttendeeRequestValidator()
    {
        RuleFor(r => r.UserId)
            .MustAsync(async (id, ct) =>
            {
                var dbContext = Resolve<AppDbContext>();
                return await dbContext.Users.AnyAsync(u => u.Id == id, cancellationToken: ct);
            })
            .WithMessage("User does not exist.");
    }
}