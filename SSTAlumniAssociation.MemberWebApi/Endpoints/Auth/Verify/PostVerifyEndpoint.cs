using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;
using SSTAlumniAssociation.Core.Context;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Auth.Verify;

public class PostVerifyEndpoint(AppDbContext dbContext) : Endpoint<PostVerifyRequest, PostVerifyResponse>
{
    public override void Configure()
    {
        Post("/Auth/Verify");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PostVerifyRequest req, CancellationToken ct)
    {
        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.Email == req.Email, cancellationToken: ct);
        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(new PostVerifyResponse
        {
            Id = user.Id,
            Linked = !string.IsNullOrWhiteSpace(user.FirebaseId)
        }, ct);
    }
}
