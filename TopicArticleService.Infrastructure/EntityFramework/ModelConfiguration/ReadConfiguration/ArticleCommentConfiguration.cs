using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ArticleCommentConfiguration : IEntityTypeConfiguration<ArticleCommentReadModel>
    {
        public void Configure(EntityTypeBuilder<ArticleCommentReadModel> builder)
        {
            //Table name
            builder.ToTable("ArticleComment");

            //Primary key
            builder.HasKey(key => key.ArticleCommentId);

            //Property config - Start

            builder.Property(p => p.Comment)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.ArticleComments)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_ArticleComments")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleComments)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleComments")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Parent)
              .WithMany(p => p.Children)
              .HasForeignKey(p => p.ParentArticleCommentId)
              .HasConstraintName("FK_ParentArticleComment_ChildArticleComments")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
