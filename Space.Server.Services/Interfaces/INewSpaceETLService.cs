using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.Interfaces
{
    public interface INewSpaceETLRequestService
    {
        Task<IEnumerable<long>> GetActiveRequests(CancellationToken cancellationToken);
        Task<ServerResult<bool>> MarkAsCompletedAsync(long id, CancellationToken cancellationToken);
        Task<ServerResult<bool>> MarkAsFailedAsync(long id, CancellationToken cancellationToken);
        Task<ServerResult<bool>> MarkAsDeletedAsync(long id, CancellationToken cancellationToken);
        Task<ServerResult<Guid>> CreateAsync(NewSpaceETLRequestDbModel entity, CancellationToken cancellationToken);
    }
}
