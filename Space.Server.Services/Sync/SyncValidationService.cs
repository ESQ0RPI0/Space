using Space.Shared.Sync;

namespace Space.Server.Services.Sync
{
    public class SyncValidationService
    {
        public SyncValidationService()
        {

        }

        public async Task<bool> ValidateSyncRequest(SyncTypes type)
        {
            switch (type)
            {
                case SyncTypes.NewSpace:
                    return true;
                case SyncTypes.SpaceFund:
                case SyncTypes.Unknown:
                default: return false;
            }
        }
    }
}
