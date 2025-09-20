using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.Interfaces.Common
{
    public interface IETLRequestService
    {
        Task<long> GetAsync(long id, CancellationToken cancellationToken);
        Task<ServerResult<bool>> MarkAsFailedAsync(long id, string message, CancellationToken cancellationToken);
        Task<ServerResult<bool>> MarkAsCompletedAsync(long id, string message, CancellationToken cancellationToken);
        Task<ServerResult<bool>> DeleteAsync(long id, string message, CancellationToken cancellationToken);
    }
}
