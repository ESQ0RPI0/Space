using Microsoft.SemanticKernel;
using Space.Client.Datamodel.ViewModels;
using Space.Server.Services.NewSpace;
using Space.Shared.Api.ApiResults;
using System.ComponentModel;

namespace Space.Server.AI.Logic.Plugins
{
    [Description("Class for NewSpace launch vehicle functions")]
    public sealed class LaunchVehiclesPlugin
    {
        private readonly NewSpaceService _service;

        public LaunchVehiclesPlugin(NewSpaceService service)
        {
            _service = service;
        }

        [KernelFunction("GetRawLaunchVehicleByCountry")]
        [Description($"Method to return list of the {nameof(NsRawLaunchVehicleViewModel)} based on a country name. Parameter to pass has a type 'string'")]
        [return: Description($"ServerResult class with List of {nameof(NsRawLaunchVehicleViewModel)} type elements")]
        public async Task<ServerResult<List<NsRawLaunchVehicleViewModel>>> GetByCountry([Description("Name of the country")]string country, CancellationToken cancellationToken = default)
        {
            return await _service.GetByCountryAsync(country, cancellationToken);
        }
    }
}
