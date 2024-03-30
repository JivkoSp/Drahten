using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserArticleConfiguration : IEntityTypeConfiguration<UserArticleReadModel>
    {
        public void Configure(EntityTypeBuilder<UserArticleReadModel> builder)
        {
            //Table name
            builder.ToTable("UserArticle");

            //Composite primary key
            builder.HasKey(key => new { key.UserId, key.ArticleId });

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.UserArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_UserArticles")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Article)
                .WithMany(p => p.UserArticles)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_UserArticles")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
