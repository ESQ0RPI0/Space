using Microsoft.EntityFrameworkCore;

namespace Space.Server.Database.Context
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            
        }
    }
}
