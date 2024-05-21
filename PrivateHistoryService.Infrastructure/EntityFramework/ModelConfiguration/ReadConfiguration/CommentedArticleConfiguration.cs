using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class CommentedArticleConfiguration : IEntityTypeConfiguration<CommentedArticleReadModel>
    {
        public void Configure(EntityTypeBuilder<CommentedArticleReadModel> builder)
        {
            //Table name
            builder.ToTable("CommentedArticle");

            //Primary key
            builder.HasKey(key => key.CommentedArticleId);

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.ArticleComment)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.CommentedArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_CommentedArticles")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
