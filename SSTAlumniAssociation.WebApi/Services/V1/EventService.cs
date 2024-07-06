using Microsoft.AspNetCore.Authorization;
using Protos.Event.V1;

namespace SSTAlumniAssociation.WebApi.Services.V1;

[Authorize]
public class EventServiceV1 : EventService.EventServiceBase
{
}
