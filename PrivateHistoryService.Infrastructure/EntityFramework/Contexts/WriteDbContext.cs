using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Contexts
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
        public DbSet<LikedArticle> LikedArticles { get; set; }
        public DbSet<DislikedArticle> DislikedArticles { get; set; }
        public DbSet<CommentedArticle> CommentedArticles { get; set; }
        public DbSet<LikedArticleComment> LikedArticleComments { get; set; }
        public DbSet<DislikedArticleComment> DislikedArticleComments { get; set; }
        public DbSet<SearchedArticleData> SearchedArticles { get; set; }
        public DbSet<TopicSubscription> TopicSubscriptions { get; set; }
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
            modelBuilder.ApplyConfiguration(new DislikedArticleCommentConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new DislikedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new LikedArticleCommentConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new LikedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedArticleDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedTopicDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new TopicSubscriptionConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedUserConfiguration(_encryptionProvider));
        }
    }
}
