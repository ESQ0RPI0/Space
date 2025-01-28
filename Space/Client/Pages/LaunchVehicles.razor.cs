using MudBlazor;
using Space.Client.Datamodel.ViewModels;
using Space.Client.Forms.Basic;
using Space.Client.Launchers;
using Space.Shared.Api.ApiResults;
using Space.Client.Shared;

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

        public ServerResult<IEnumerable<NsRawItemViewModel>>? RawResult { get; private set; }

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

        private async Task HandlePromptAsync(string prompt)
        {

        }

        private async Task Sync()
        {
            await _client.SyncLaunchVehicles();
            await GetData();
        }

        private async Task GetData()
        {
            RawResult = await _client.GetLaunchVehicles(PagingForm);

            if (RawResult == null)
            {
                _snackbar.Add("Error occured during HTTP request");
                return;
            }

            if (RawResult.IsCorrect == false && RawResult.Information is not null)
            {
                _snackbar.Add(RawResult.Information.Message);
            }

            _snackbar.Add("Raw LV data loaded");
        }
    }
}