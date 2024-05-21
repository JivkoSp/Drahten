using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class LikedArticleConfiguration : IEntityTypeConfiguration<LikedArticleReadModel>
    {
        public void Configure(EntityTypeBuilder<LikedArticleReadModel> builder)
        {
            //Table name
            builder.ToTable("LikedArticle");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleId, key.UserId });

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
                .WithMany(p => p.LikedArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_LikedArticles")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
