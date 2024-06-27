using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ReadDbContext(DbContextOptions<ReadDbContext> options, IEncryptionProvider encryptionProvider) : base(options)
        {
            _encryptionProvider = encryptionProvider;
        }

        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<BannedUserReadModel> BannedUsers { get; set; }
        public DbSet<ContactRequestReadModel> ContactRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("user-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the entity models
            modelBuilder.ApplyConfiguration(new UserConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new BannedUserConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ContactRequestConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new UserTrackingConfiguration(_encryptionProvider));
        }
    }
}
