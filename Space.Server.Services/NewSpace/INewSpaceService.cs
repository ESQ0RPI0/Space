using Space.Backend.Datamodel.Models.NewSpace;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.NewSpace
{
    public interface INewSpaceService
    {
        Task<ServerResult<bool>> AddExternalListItem(NewSpaceExternalListItemModel item);
        Task<ServerResult<List<NsRawLaunchVehicleViewModel>>> GetByCountryAsync(string country, CancellationToken cancellationToken);
        Task<ServerResult<List<NsRawItemViewModel>>> GetRawList(PagingForm form, CancellationToken cancellationToken);
        Task<ServerResult<NsRawLaunchVehicleViewModel>> GetRawVehicle(int id, CancellationToken cancellationToken);
    }
}