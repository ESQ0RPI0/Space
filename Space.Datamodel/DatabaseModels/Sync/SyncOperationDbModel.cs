using Space.Shared.Sync;
using System.ComponentModel.DataAnnotations;

namespace Space.Server.Datamodel.DatabaseModels.Sync
{
    public class SyncOperationDbModel
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? EnqueueDate { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public DateTimeOffset? LastUpdateDate { get; set; }
        public SyncStates State { get; set; }
        public SyncProcessStages Stage { get; set; }
        public SyncTypes Type { get; set; }
    }
}
