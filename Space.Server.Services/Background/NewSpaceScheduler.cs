using Hangfire;
using Space.Server.Services.Interfaces.Common;
using Space.Server.Services.NewSpace;
using Space.Shared.Api.ApiResults;

namespace Space.Server.Services.Background
{
    internal sealed class NewSpaceScheduler
    {
        private readonly IRecurrentProcessor _recurrentProcessor;

        public NewSpaceScheduler(IRecurrentProcessor recurrentProcessor)
        {
            _recurrentProcessor = recurrentProcessor;
        }

        public async Task<bool> EnqueueLauncherDataMergeAsync()
        {
            var guid = _recurrentProcessor.AddOrUpdate<NewSpaceETLProcessor>(x => x.StartAsync(default), Cron.Minutely(), NewSpaceETLProcessor.NewSpace_ETL);

            return ServerResults.CachedTrue.Result;
        }
    }
}
