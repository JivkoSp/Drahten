using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<BannedUserReadModel> BannedUsers { get; set; }
        public DbSet<ContactRequestReadModel> ContactRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the entity models
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BannedUserConfiguration());
            modelBuilder.ApplyConfiguration(new ContactRequestConfiguration());
            modelBuilder.ApplyConfiguration(new UserTrackingConfiguration());
        }
    }
}
