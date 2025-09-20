using HtmlAgilityPack;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Sync.Interfaces
{
    public interface IPageLoadService
    {
        Task<ServerResult<HtmlDocument?>> LoadAsync(string url, CancellationToken cancellationToken);
    }
}
