using Drahten_Services_UserService.Models;
using Drahten_Services_UserService.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Drahten_Services_UserService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public virtual DbSet<Article>? Articles { get; set; }
        public virtual DbSet<ArticleComment>? ArticleComments { get; set; }
        public virtual DbSet<ArticleLike>? ArticleLikes { get; set; }
        public virtual DbSet<ContactRequest>? ContactRequests { get; set; }
        public virtual DbSet<PrivateHistory>? PrivateHistories { get; set; }
        public virtual DbSet<PublicHistory>? PublicHistories { get; set; }
        public virtual DbSet<SearchedTopicDataPrivateHist>? SearchedTopicDataPrivateHists { get; set; }
        public virtual DbSet<SearchedTopicDataPublicHist>? SearchedTopicDataPublicHists { get; set; }
        public virtual DbSet<Topic>? Topics { get; set; }
        public virtual DbSet<User>? Users  { get; set; }
        public virtual DbSet<UserArticle>? UserArticles { get; set; }
        public virtual DbSet<UserTopic>? UserTopics { get; set; }
        public virtual DbSet<UserTracking>? UserTrackings { get; set; }
        public virtual DbSet<ViewedArticlePrivateHist>? ViewedArticlePrivateHists { get; set; }
        public virtual DbSet<ViewedArticlePublicHist>? ViewedArticlePublicHists { get; set; }
        public virtual DbSet<ViewedUserPrivateHist>? ViewedUserPrivateHists { get; set; }
        public virtual DbSet<ViewedUserPublicHist>? ViewedUserPublicHists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ArticleCommentConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleLikeConfiguration());
            modelBuilder.ApplyConfiguration(new ContactRequestConfiguration());
            modelBuilder.ApplyConfiguration(new PrivateHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new PublicHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new SearchedTopicDataPrivateHistConfiguration());
            modelBuilder.ApplyConfiguration(new SearchedTopicDataPublicHistConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new UserArticleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserTopicConfiguration());
            modelBuilder.ApplyConfiguration(new UserTrackingConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedArticlePrivateHistConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedArticlePublicHistConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedUserPrivateHistConfiguration());
            modelBuilder.ApplyConfiguration(new ViewedUserPublicHistConfiguration());
        }
    }
}
