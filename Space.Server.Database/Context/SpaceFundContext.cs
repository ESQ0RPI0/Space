using Microsoft.EntityFrameworkCore;

namespace Space.Server.Database.Context
{
    public class SpaceFundContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
