using MediatR;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Sync.Processes.SpaceFund.Requests
{
    public sealed record SfStartProcessRequest() : IRequest<ServerResult<bool>>;
}
