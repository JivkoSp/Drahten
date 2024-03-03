using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ArticleCommentThumbsDownConfiguration : IEntityTypeConfiguration<ArticleCommentThumbsDown>
    {
        public void Configure(EntityTypeBuilder<ArticleCommentThumbsDown> builder)
        {
            //Table name
            builder.ToTable("ArticleCommentThumbsDown");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentId, key.UserId });

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleCommentThumbsDown)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleCommentThumbsDown")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ArticleComment)
                .WithMany(p => p.ArticleCommentThumbsDown)
                .HasForeignKey(p => p.ArticleCommentId)
                .HasConstraintName("FK_ArticleComment_ArticleCommentThumbsDown")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
