using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration;

namespace UserService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IEncryptionProvider _encryptionProvider;

        public WriteDbContext(DbContextOptions<WriteDbContext> options, ILoggerFactory loggerFactory,
            IEncryptionProvider encryptionProvider) : base(options)
        {
            _loggerFactory = loggerFactory;
            _encryptionProvider = encryptionProvider;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(_loggerFactory); // Enable logging
                optionsBuilder.EnableSensitiveDataLogging(); // Include parameter values in logs
            }
        }

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
