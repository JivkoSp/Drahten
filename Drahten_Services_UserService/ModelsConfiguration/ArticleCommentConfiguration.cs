using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ArticleCommentConfiguration : IEntityTypeConfiguration<ArticleComment>
    {
        public void Configure(EntityTypeBuilder<ArticleComment> builder)
        {
            //Table name
            builder.ToTable("ArticleComment");

            //Primary key
            builder.HasKey(key => key.ArticleCommentId);

            //Property config - Start

            builder.Property(p => p.Comment)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.ArticleComments)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_ArticleComment")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleComments)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleComment")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Parent)
              .WithMany(p => p.Children)
              .HasForeignKey(p => p.ParentArticleCommentId)
              .HasConstraintName("FK_ParentArticleComment_ChildArticleComments")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
