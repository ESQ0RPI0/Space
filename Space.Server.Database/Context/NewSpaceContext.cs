using Microsoft.EntityFrameworkCore;
using Space.Server.Datamodel.DatabaseModels.NewSpace;

namespace Space.Server.Database.Context
{
    //To add a migration take a look in settings file, there should be something like 
    public class NewSpaceContext : DbContext
    {
        public NewSpaceContext(DbContextOptions<NewSpaceContext> options): base(options)
        {
            
        }

        public DbSet<NewSpaceLaunchVehicleDbModel> NewSpaceLaunchVehicles { get; set; }
        public DbSet<NewSpaceCompanyDbModel> NewSpaceCompanies { get; set;}
        public DbSet<NewSpaceExternalListItemDbModel> NewSpaceExternalListItems { get; set; }
    }
}
