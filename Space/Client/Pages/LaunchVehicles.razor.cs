using Microsoft.AspNetCore.Components;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.FrontHttpClient.Launchers;

namespace Space.Client.Pages
{
    public partial class LaunchVehicles
    {
        [Inject]
        private LaunchVehiclesApiClient Client { get; set; }

        private PagingForm PagingForm = new PagingForm 
        {
            Count = 30,
            Offset = 0
        };

        private List<LaunchVehicleViewModel> LaunchVehiclesList = new List<LaunchVehicleViewModel>();

        public LaunchVehicles() { }
        

        protected override async Task OnInitializedAsync()
        { 
            await base.OnInitializedAsync();

            LaunchVehiclesList = await Client.GetLaunchVehicles(PagingForm);
        }

        private async Task Sync()
        {
            await Client.SyncLaunchVehicles();

            LaunchVehiclesList = await Client.GetLaunchVehicles(PagingForm);
        }
    }
}