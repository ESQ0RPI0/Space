using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.FrontHttpClient.Launchers;

namespace Space.Client.Pages
{
    public partial class LaunchVehicles
    {
        private readonly LaunchVehiclesApiClient _client;

        private PagingForm PagingForm = new PagingForm 
        {
            Count = 30,
            Offset = 0
        };

        private List<LaunchVehicleViewModel> LaunchVehiclesList = new List<LaunchVehicleViewModel>();

        public LaunchVehicles(LaunchVehiclesApiClient _httpClient)
        {
            _client = _httpClient;
        }

        protected override async Task OnInitializedAsync()
        { 
            await base.OnInitializedAsync();

            LaunchVehiclesList = await _client.GetLaunchVehicles(PagingForm);
        }
    }
}