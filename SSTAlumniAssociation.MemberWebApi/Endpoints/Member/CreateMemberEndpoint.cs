using FastEndpoints;
using SSTAlumniAssociation.Core.Dtos.User;

namespace SSTAlumniAssociation.MemberWebApi.Endpoints.Member;

public class CreateMemberEndpoint: Endpoint<CreateMemberRequest, UserResponse>
{
    public override void Configure()
    {
        Post("/Member");
    }

    public override async Task HandleAsync(CreateMemberRequest req, CancellationToken ct)
    {
        switch (req)
        {
            case CreateAlumniMemberRequest am:
            {
                break;
            }
            case CreateEmployeeMemberRequest em:
            {
                break;
            }
            default:
                    throw new Exception();
        }
    }
}