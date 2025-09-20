using HtmlAgilityPack;
using MediatR;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Sync.Processes.NewSpace.Requests
{
    internal sealed record NsPageLoadRequest() : IRequest<ServerResult<HtmlDocument?>>;
}
