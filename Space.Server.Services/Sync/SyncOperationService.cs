using Space.Server.Datamodel.DatabaseModels.Sync;
using Space.Server.Sync.Database.Context;
using Space.Shared.Api.ApiResults;
using Space.Shared.Sync;
using System.ComponentModel;
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

        public async Task<ServerResult<int>> InitSync(SyncTypes type)
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

        public async Task<ServerResult<bool>> ChangeState(int id, SyncStates state)
        {
            var syncOperation = _dc.SyncOperations.Find(id);
            if (syncOperation == null) 
                return ServerResults.CachedFalse;

            await ChangeSyncOperationInternal(syncOperation, u => u.State = state);

            return ServerResults.CachedTrue;
        }

        public async Task<ServerResult<bool>> ChangeStage(int id, SyncProcessStages stage)
        {
            var syncOperation = _dc.SyncOperations.Find(id);
            if (syncOperation == null)
                return ServerResults.CachedFalse;

            await ChangeSyncOperationInternal(syncOperation, u => u.Stage  = stage);

            return ServerResults.CachedTrue;
        }

        public async Task<ServerResult<bool>> ChangeSyncOperation(int id, SyncStates state, SyncProcessStages stage)
        {
            var syncOperation = _dc.SyncOperations.Find(id);
            if (syncOperation == null)
                return ServerResults.CachedFalse;

            await ChangeSyncOperationInternal(syncOperation, u => 
            {
                u.Stage = stage;
                u.State = state;
            });

            return ServerResults.CachedTrue;
        }

        private async Task<ServerResult<bool>> ChangeSyncOperationInternal(SyncOperationDbModel entity, Action<SyncOperationDbModel> setFunction)
        {
            setFunction(entity);
            entity.LastUpdateDate = DateTime.UtcNow;

            await _dc.SaveChangesAsync();

            return ServerResults.CachedTrue;
        }
    }
}
