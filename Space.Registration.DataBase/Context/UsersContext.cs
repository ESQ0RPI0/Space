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
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired();

        }
    }
}
