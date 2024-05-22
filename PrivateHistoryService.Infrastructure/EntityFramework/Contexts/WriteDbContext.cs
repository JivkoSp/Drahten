using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class WriteDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public WriteDbContext(DbContextOptions<WriteDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public DbSet<User> Users { get; set; }

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
