using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.User;

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
