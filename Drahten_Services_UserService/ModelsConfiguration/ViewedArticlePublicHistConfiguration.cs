using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ViewedArticlePublicHistConfiguration : IEntityTypeConfiguration<ViewedArticlePublicHist>
    {
        public void Configure(EntityTypeBuilder<ViewedArticlePublicHist> builder)
        {
            //Table name
            builder.ToTable("ViewedArticlePublicHist");

            //Primary key
            builder.HasKey(key => key.ArticleId);

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .ValueGeneratedNever();

            builder.Property(p => p.ViewTime)
             .HasColumnType("timestamp without time zone")
             .ValueGeneratedOnAddOrUpdate()
             .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Article)
                .WithOne()
                .HasForeignKey<ViewedArticlePublicHist>(p => p.ArticleId)
                .HasConstraintName("FK_Article_ViewedArticlePublicHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PublicHistory)
                .WithMany(p => p.ViewedArticles)
                .HasForeignKey(p => p.PublicHistoryId)
                .HasConstraintName("FK_PublicHistory_ViewedArticlePublicHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
