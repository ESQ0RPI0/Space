using MudBlazor;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.FrontHttpClient.Launchers;
using Space.Shared.Api.ApiResults;

namespace Space.Client.Pages
{
    public partial class LaunchVehicles
    {
        private readonly LaunchVehiclesApiClient _client;
        private readonly ISnackbar _snackbar;

        private PagingForm PagingForm = new PagingForm
        {
            Count = 30,
            Offset = 0
        };

        public ServerResult<IEnumerable<LaunchVehicleRawViewModel>>? Result { get; private set; }

        public LaunchVehicles(LaunchVehiclesApiClient client, ISnackbar snackbar)
        {
            _client = client;
            _snackbar = snackbar;
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await GetData();
        }

        private async Task Sync()
        {
            await _client.SyncLaunchVehicles();
            await GetData();
        }

        private async Task GetData()
        {
            Result = await _client.GetLaunchVehicles(PagingForm);

            if (Result == null)
            {
                _snackbar.Add("Error occured dureng HTTP request");
                return;
            }

            if (Result.IsCorrect == false && Result.Information is not null)
            {
                _snackbar.Add(Result.Information.Message);
            }
        }
    }
}