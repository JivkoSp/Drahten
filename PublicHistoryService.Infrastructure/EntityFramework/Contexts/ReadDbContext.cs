using Microsoft.EntityFrameworkCore;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PublicHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.EntityFramework.Contexts
{
    internal sealed class ReadDbContext : DbContext
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ReadDbContext(DbContextOptions<ReadDbContext> options, IEncryptionProvider encryptionProvider) : base(options)
        {
            _encryptionProvider = encryptionProvider;
        }

        public DbSet<UserReadModel> Users { get; set; }
        public DbSet<CommentedArticleReadModel> CommentedArticles { get; set; }
        public DbSet<SearchedArticleDataReadModel> SearchedArticles { get; set; }
        public DbSet<SearchedTopicDataReadModel> SearchedTopics { get; set; }
        public DbSet<ViewedArticleReadModel> ViewedArticles { get; set; }
        public DbSet<ViewedUserReadModel> ViewedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("private-history-service");

            base.OnModelCreating(modelBuilder);

            //Applying configurations for the entity models
            modelBuilder.ApplyConfiguration(new CommentedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedArticleDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new SearchedTopicDataConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new UserConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedArticleConfiguration(_encryptionProvider));
            modelBuilder.ApplyConfiguration(new ViewedUserConfiguration(_encryptionProvider));
        }
    }
}
