using Microsoft.EntityFrameworkCore;
using PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

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
