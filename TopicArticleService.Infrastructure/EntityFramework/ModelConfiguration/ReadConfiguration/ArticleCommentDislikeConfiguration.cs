using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ArticleCommentDislikeConfiguration : IEntityTypeConfiguration<ArticleCommentDislikeReadModel>
    {
        public void Configure(EntityTypeBuilder<ArticleCommentDislikeReadModel> builder)
        {
            //Table name
            builder.ToTable("ArticleCommentDislike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentId, key.UserId });

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleCommentDislikes)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleCommentDislikes")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ArticleComment)
                .WithMany(p => p.ArticleCommentDislikes)
                .HasForeignKey(p => p.ArticleCommentId)
                .HasConstraintName("FK_ArticleComment_ArticleCommentDislikes")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
