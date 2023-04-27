using Microsoft.EntityFrameworkCore;
using Space.Server.Datamodel.DatabaseModels.NewSpace;

namespace Space.Server.Database.Context
{
    public class NewSpaceContext : DbContext
    {
        public NewSpaceContext(DbContextOptions<NewSpaceContext> options): base(options)
        {
            
        }

        public DbSet<NewSpaceLaunchVehicleDbModel> NewSpaceLaunchVehicles { get; set; }
        public DbSet<NewSpaceCompanyDbModel> NewSpaceCompanies { get; set;}
    }
}
