using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration;

namespace TopicArticleService.Infrastructure.EntityFramework.Contexts
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

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleComment> ArticleComments { get; set; }
        public DbSet<Topic> Topics { get; set; }
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
            modelBuilder.HasDefaultSchema("topic-article-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the entity models - START 

            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleLikeConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ArticleDislikeConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ArticleCommentConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ArticleCommentLikeConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ArticleCommentDislikeConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTopicConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new UserArticleConfiguration());

            //Applying configurations for the entity models - END
        }
    }
}
