using MudBlazor;
using Space.Shared.Api.ApiResults;

namespace Space.Client.Services.Mud
{
    public class MudSnackbarDecoratorService
    {
        private readonly ISnackbar _snackbar;

        public MudSnackbarDecoratorService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void ShowMessage<T>(ServerResult<T> result)
        {
            _snackbar.Add(message: result.Information.Message, severity: Severity.Info);
        }
    }
}
