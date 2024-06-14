using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ReadDbContext(DbContextOptions<ReadDbContext> options, IEncryptionProvider encryptionProvider) : base(options)
        {
            _encryptionProvider = encryptionProvider;
        }

        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<DislikedArticleCommentReadModel> DislikedArticleComments { get; set; }
        public DbSet<LikedArticleCommentReadModel> LikedArticleComments { get; set; }
        public DbSet<DislikedArticleReadModel> DislikedArticles { get; set; }
        public DbSet<LikedArticleReadModel> LikedArticles { get; set; }
        public DbSet<CommentedArticleReadModel> CommentedArticles { get; set; }
        public DbSet<SearchedArticleDataReadModel> SearchedArticles { get; set; }
        public DbSet<SearchedTopicDataReadModel> SearchedTopics { get; set; }
        public DbSet<TopicSubscriptionReadModel> TopicSubscriptions { get; set; }
        public DbSet<ViewedArticleReadModel> ViewedArticles { get; set; }
        public DbSet<ViewedUserReadModel> ViewedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("private-history-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the entity models
            modelBuilder.ApplyConfiguration(new CommentedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new DislikedArticleCommentConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new DislikedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new LikedArticleCommentConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new LikedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedArticleDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedTopicDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new TopicSubscriptionConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new UserConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedUserConfiguration(_encryptionProvider));
        }
    }
}
