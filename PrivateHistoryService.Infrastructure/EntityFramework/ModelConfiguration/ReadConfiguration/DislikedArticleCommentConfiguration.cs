using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class DislikedArticleCommentConfiguration : IEntityTypeConfiguration<DislikedArticleCommentReadModel>
    {
        public void Configure(EntityTypeBuilder<DislikedArticleCommentReadModel> builder)
        {
            //Table name
            builder.ToTable("DislikedArticleComment");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentId, key.UserId });

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.DislikedArticleComments)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_DislikedArticleComments")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
