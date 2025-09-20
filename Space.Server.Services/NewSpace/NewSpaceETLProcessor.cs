using Hangfire;
using Microsoft.Extensions.Logging;
using Space.Server.Services.Interfaces;

namespace Space.Server.Services.NewSpace
{
    /// <summary>
    /// Hangfire-related Service for NewSpace oeprations scheduling
    /// </summary>
    internal sealed class NewSpaceETLProcessor
    {
        public const string NewSpace_ETL = "NewSpace_ETL";

        private readonly INewSpaceService _newSpaceService;
        private readonly INewSpaceQueryService _newSpaceQueryService;
        private readonly ILogger<NewSpaceETLProcessor> _logger;
        private readonly INewSpaceETLRequestService _etlRequests;

        private static bool isInProcess;
        public NewSpaceETLProcessor(INewSpaceService newSpaceService, INewSpaceQueryService newSpaceQueryService, ILogger<NewSpaceETLProcessor> logger,
            INewSpaceETLRequestService etlRequests)
        {
            _newSpaceService = newSpaceService;
            _newSpaceQueryService = newSpaceQueryService;
            _logger = logger;
            _etlRequests = etlRequests;
        }

        [Queue(NewSpace_ETL)]
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                isInProcess = true;


            }
            catch (Exception e)
            {
                _logger.LogError(message: $"{nameof(NewSpaceETLProcessor)}: Error occured during NewSpace operation", exception: e);
                return;
            }
            finally
            {
                isInProcess = false;
            }
        }
    }
}
