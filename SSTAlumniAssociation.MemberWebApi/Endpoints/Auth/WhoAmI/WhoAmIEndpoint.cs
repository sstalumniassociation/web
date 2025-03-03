using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using SSTAlumniAssociation.Core.Context;
using SSTAlumniAssociation.Core.Dtos.User;
using SSTAlumniAssociation.Core.Extensions;
using SSTAlumniAssociation.MemberWebApi.Endpoints.User;
using SSTAlumniAssociation.MemberWebApi.Mappers;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Auth.WhoAmI;

public class WhoAmIEndpoint(AppDbContext dbContext) : EndpointWithoutRequest<UserResponse>
{
    public override void Configure()
    {
        Get("/Auth/WhoAmI");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var user = await dbContext.Users
            .WhereUserMatchesEmailFromClaims(HttpContext.User.Claims)
            .SingleAsync(cancellationToken: ct);

        await SendOkAsync(user.ToResponse(), ct);
    }
}