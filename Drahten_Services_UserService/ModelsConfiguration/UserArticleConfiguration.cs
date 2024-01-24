using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class UserArticleConfiguration : IEntityTypeConfiguration<UserArticle>
    {
        public void Configure(EntityTypeBuilder<UserArticle> builder)
        {
            //Table name
            builder.ToTable("UserArticle");

            //Composite primary key
            builder.HasKey(key => new { key.UserId, key.ArticleId });

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.UserArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_UserArticle")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Article)
                .WithMany(p => p.UserArticles)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_UserArticle")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
