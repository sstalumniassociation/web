using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.User;

/// <summary>
/// Bind user to a Firebase ID. Although this can also be done in the UpdateUser route, due to the different
/// permission requirement for updating FirebaseId, it would introduce a partial success state for the endpoint (where
/// only the FirebaseId is updated of all fields provided in the UpdateMask). Therefore, a separate route exists for
/// this specific use case. This route should be called by end-users only (not admins) as it will bind the user ID
/// provided to the current authenticated user.
/// </summary>
public class BindUserEndpoint(AppDbContext dbContext) : Endpoint<BindUserRequest, UserResponse>
{
    public override void Configure()
    {
        Post("/User/{Id:guid}/Bind");
    }

    public override async Task HandleAsync(BindUserRequest req, CancellationToken ct)
    {
        var email = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Email);
        if (email is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var firebaseId = User.Claims.SingleOrDefault(c => c.Type == "user_id");
        if (firebaseId is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Id == req.Id, cancellationToken: ct);
        if (user is null || user.Email != email.Value)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        if (!string.IsNullOrWhiteSpace(user.FirebaseId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        user.FirebaseId = firebaseId.Value;

        await dbContext.SaveChangesAsync(ct);

        await SendOkAsync(user.ToResponse(), ct);
    }
}