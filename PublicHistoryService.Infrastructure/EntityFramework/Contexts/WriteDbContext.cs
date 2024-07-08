using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PublicHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration;

namespace PublicHistoryService.Infrastructure.EntityFramework.Contexts
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
        public DbSet<CommentedArticle> CommentedArticles { get; set; }
        public DbSet<SearchedArticleData> SearchedArticles { get; set; }
        public DbSet<ViewedArticle> ViewedArticles { get; set; }

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
            modelBuilder.HasDefaultSchema("private-history-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the domain entities and value objects
            modelBuilder.ApplyConfiguration(new CommentedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedArticleDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedTopicDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new UserConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedUserConfiguration(_encryptionProvider));
        }
    }
}
