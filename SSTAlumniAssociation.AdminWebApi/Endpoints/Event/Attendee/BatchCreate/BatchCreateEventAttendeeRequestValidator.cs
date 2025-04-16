using FastEndpoints;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.AdminWebApi.Endpoints.Event.Attendee.BatchCreate;

public class BatchCreateEventAttendeeRequestValidator : Validator<BatchCreateEventAttendeeRequest>
{
    public BatchCreateEventAttendeeRequestValidator()
    {
        RuleFor(r => r.UserIds)
            .MustAsync(async (ids, ct) =>
            {
                var dbContext = Resolve<AppDbContext>();
                var count = await dbContext.Users.Where(u => ids.Contains(u.Id)).CountAsync(cancellationToken: ct);
                return count == ids.Count();
            })
            .WithMessage("A user does not exist in provided batch.");
    }
}