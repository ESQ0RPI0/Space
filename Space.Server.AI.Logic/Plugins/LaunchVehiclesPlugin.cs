using Microsoft.SemanticKernel;
using Space.Client.Datamodel.ViewModels;
using Space.Server.Services.Interfaces;
using Space.Shared.Api.ApiResults;
using System.ComponentModel;

namespace Space.Server.AI.Logic.Plugins
{
    public class LaunchVehiclesPlugin
    {
        private readonly INewSpaceQueryService _service;

        public LaunchVehiclesPlugin(INewSpaceQueryService service)
        {
            _service = service;
        }

        [KernelFunction]
        [Description($"List of the launch vehicles by a country name")]
        public async Task<List<NsRawLaunchVehicleViewModel>> GetByCountry([Description("Name of the country")]string country,
            [Description("Token to cancel the operation")] CancellationToken cancellationToken = default)
        {
            var result = await _service.GetVehiclesByCountryAsync(country, cancellationToken);

            return result.Result;
        }
    }
}
