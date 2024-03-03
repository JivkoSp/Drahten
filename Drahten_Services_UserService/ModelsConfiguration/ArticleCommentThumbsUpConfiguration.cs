using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ArticleCommentThumbsUpConfiguration : IEntityTypeConfiguration<ArticleCommentThumbsUp>
    {
        public void Configure(EntityTypeBuilder<ArticleCommentThumbsUp> builder)
        {
            //Table name
            builder.ToTable("ArticleCommentThumbsUp");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentId, key.UserId });

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleCommentThumbsUp)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleCommentThumbsUp")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ArticleComment)
                .WithMany(p => p.ArticleCommentThumbsUp)
                .HasForeignKey(p => p.ArticleCommentId)
                .HasConstraintName("FK_ArticleComment_ArticleCommentThumbsUp")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
