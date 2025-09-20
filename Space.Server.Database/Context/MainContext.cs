using Microsoft.EntityFrameworkCore;
using Space.Server.Datamodel.DatabaseModels.Main.ETL;

namespace Space.Server.Database.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            
        }

        public DbSet<ETLRequestDbModel> ETLRequests { get; set; }
    }
}
