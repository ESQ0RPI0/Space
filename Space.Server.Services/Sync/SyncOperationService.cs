using Space.Server.Datamodel.DatabaseModels.Sync;
using Space.Server.Sync.Database.Context;
using Space.Shared.Api.ApiResults;
using Space.Shared.Sync;
using static Space.Shared.Common.Server.ServerTypes;

namespace Space.Server.Services.Sync
{
    public class SyncOperationService
    {
        private readonly SyncDbContext _dc;
        private readonly SyncValidationService _validationService;

        public SyncOperationService(SyncDbContext dc, SyncValidationService validationService)
        {
            _dc = dc;
            _validationService = validationService;
        }

        public async Task<ServerResult<int>> StartSync(SyncTypes type)
        {
            var isValid = await _validationService.ValidateSyncRequest(type);
            if (!isValid)
                return ServerErrorCodes.SyncOperationRequestInvalid;

            var newRequest = new SyncOperationDbModel
            {
                CreatedDate = DateTime.UtcNow,
                Type = type,
                Stage = SyncProcessStages.ExternalDataLoading,
                State = SyncStates.Created
            };

            var addResult = _dc.SyncOperations.Add(newRequest);
            await _dc.SaveChangesAsync();

            return addResult.Entity.Id;
        }
    }
}
