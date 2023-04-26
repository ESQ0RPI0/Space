using Microsoft.EntityFrameworkCore;
using Space.Server.Datamodel.DatabaseModels.Registration;

namespace Space.Registration.DataBase.Context
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
   
        }


    }
}
