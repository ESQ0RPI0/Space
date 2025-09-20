using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.Server.Datamodel.DatabaseModels.NewSpace;
using Space.Shared.Api.ApiResults;
using System.Linq.Expressions;

namespace Space.Server.Services.Interfaces
{
    public interface INewSpaceQueryService
    {
        Task<ServerResult<List<NsRawLaunchVehicleViewModel>>> GetVehiclesByCountryAsync(string country, CancellationToken cancellationToken);
        Task<ServerResult<List<NsRawItemViewModel>>> GetRawViewModelByForm(PagingForm form, CancellationToken cancellationToken);
        Task<ServerResult<IEnumerable<NewSpaceExternalListItemDbModel>>> GetRawListByPredicate(Expression<Func<NewSpaceExternalListItemDbModel, bool>> predicate, CancellationToken cancellationToken);
        Task<ServerResult<NsRawLaunchVehicleViewModel>> GetVehicle(int id, CancellationToken cancellationToken);
    }
}
