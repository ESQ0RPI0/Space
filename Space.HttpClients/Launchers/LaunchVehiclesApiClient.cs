using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.HttpClients;

namespace Space.FrontHttpClient.Launchers
{
    public class LaunchVehiclesApiClient
    {
        private readonly FrontApiClient _frontApiClient;

        public LaunchVehiclesApiClient(FrontApiClient _httpClient)
        {
            _frontApiClient = _httpClient;
        }

        public async Task<List<LaunchVehicleViewModel>> GetLaunchVehicles(PagingForm form)
        {
            return await _frontApiClient.Get<List<LaunchVehicleViewModel>>("NewSpace/List", form);
        }
    }
}
