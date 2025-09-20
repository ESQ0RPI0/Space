using Space.Backend.Datamodel.Models.NewSpace;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.Interfaces
{
    /// <summary>
    /// NewSpace entities service
    /// </summary>
    public interface INewSpaceService
    {
        Task<ServerResult<bool>> AddOrUpdateExternalListItems(IEnumerable<NsExternalListItemModel> items, CancellationToken cancellationToken);
    }
}