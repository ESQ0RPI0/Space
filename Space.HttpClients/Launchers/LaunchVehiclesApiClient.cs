using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.HttpClients;
using Space.Shared.Api.ApiResults;

namespace Space.Client.Launchers
{
    public class LaunchVehiclesApiClient
    {
        private readonly FrontApiClient _frontApiClient;

        public LaunchVehiclesApiClient(FrontApiClient _httpClient)
        {
            _frontApiClient = _httpClient;
        }

        public async Task<ServerResult<IEnumerable<NsRawItemViewModel>>> GetLaunchVehicles(PagingForm form)
        {
            return await _frontApiClient.Get<ServerResult<IEnumerable<NsRawItemViewModel>>>("NewSpace/RawList", form);
        }

        public async Task<ServerResult<bool>> SyncLaunchVehicles()
        {
            return await _frontApiClient.Get<ServerResult<bool>>("NewSpace/RunSync");
        }
    }
}
