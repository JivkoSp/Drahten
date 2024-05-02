using Microsoft.EntityFrameworkCore;
using TopicArticleService.Infrastructure.EntityFramework.Models;
using TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;
using TopicArticleService.Infrastructure.Extensions;

namespace TopicArticleService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
        {
        }

        public DbSet<ArticleReadModel> Articles { get; set; }
        public DbSet<ArticleLikeReadModel> ArticleLikes { get; set; }
        public DbSet<ArticleDislikeReadModel> ArticleDislikes { get; set; }
        public DbSet<ArticleCommentReadModel> ArticleComments { get; set; }
        public DbSet<TopicReadModel> Topics { get; set; }
        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<UserArticleReadModel> UserArticles { get; set; }
        public DbSet<UserTopicReadModel> UserTopics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("topic-article-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the entity models - START 

            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleLikeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleDislikeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCommentConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCommentLikeConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleCommentDislikeConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTopicConfiguration());
            modelBuilder.ApplyConfiguration(new UserArticleConfiguration());

            //Applying configurations for the entity models - END

            //Preparing seed data.
            var topic1 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Cybersecurity", TopicFullName = "cybersecurity" };
            var topic2 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Programming", TopicFullName = "programming" };
            var topic3 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "News", TopicFullName = "cybersecurity_news", ParentTopicId = topic1.TopicId };
            var topic4 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Projects", TopicFullName = "cybersecurity_projects", ParentTopicId = topic1.TopicId };
            var topic5 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Laws", TopicFullName = "cybersecurity_laws", ParentTopicId = topic1.TopicId };
            var topic6 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Law regulations", TopicFullName = "cybersecurity_law_regulations", ParentTopicId = topic1.TopicId };
            var topic7 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "News", TopicFullName = "programming_news", ParentTopicId = topic2.TopicId };
            var topic8 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Projects", TopicFullName = "programming_projects", ParentTopicId = topic2.TopicId };
            var topic9 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "America", TopicFullName = "cybersecurity_news_america", ParentTopicId = topic3.TopicId };
            var topic10 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Asia", TopicFullName = "cybersecurity_news_asia", ParentTopicId = topic3.TopicId };
            var topic11 = new TopicReadModel { TopicId = Guid.NewGuid(), TopicName = "Europe", TopicFullName = "cybersecurity_news_europe", ParentTopicId = topic3.TopicId };


            //Seeding information for Topics to the database.
            modelBuilder.SeedData(new List<TopicReadModel> {
                topic1, topic2, topic3, topic4, topic5, topic6,
                topic7, topic8, topic9, topic10, topic11,
            });
        }
    }
}
