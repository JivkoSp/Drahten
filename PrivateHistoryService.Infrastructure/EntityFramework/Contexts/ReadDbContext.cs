using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

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
            modelBuilder.ApplyConfiguration(new CommentedArticleConfiguration());
            modelBuilder.ApplyConfiguration(new DislikedArticleCommentConfiguration());
            modelBuilder.ApplyConfiguration(new DislikedArticleConfiguration());
            modelBuilder.ApplyConfiguration(new LikedArticleCommentConfiguration());
            modelBuilder.ApplyConfiguration(new LikedArticleConfiguration());
            modelBuilder.ApplyConfiguration(new SearchedArticleDataConfiguration());
            modelBuilder.ApplyConfiguration(new SearchedTopicDataConfiguration());
            modelBuilder.ApplyConfiguration(new TopicSubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedUserConfiguration());
        }
    }
}
