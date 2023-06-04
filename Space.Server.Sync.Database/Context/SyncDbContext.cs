using Microsoft.EntityFrameworkCore;
using Space.Server.Datamodel.DatabaseModels.Sync;

namespace Space.Server.Sync.Database.Context
{
    public class SyncDbContext : DbContext
    {
        public SyncDbContext(DbContextOptions<SyncDbContext> options) : base(options)
        {

        }
        public DbSet<SyncOperationDbModel> SyncOperations { get; set; }
    }
}
