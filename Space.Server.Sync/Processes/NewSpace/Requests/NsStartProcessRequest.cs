using MediatR;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Sync.Processes.NewSpace.Requests
{
    public sealed record NsStartProcessRequest() : IRequest<ServerResult<bool>>;
}
