using Microsoft.EntityFrameworkCore;
using Space.Server.Datamodel.DatabaseModels.NewSpace;

namespace Space.Server.Database.Context
{
    public class NewSpaceContext : DbContext
    {
        public NewSpaceContext(DbContextOptions options): base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<NewSpaceLaunchVehicleDbModel> NewSpaceLaunchVehicles { get; set; }
        public DbSet<NewSpaceCompanyDbModel> NewSpaceCompanies { get; set;}
    }
}
