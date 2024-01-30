using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ViewedArticlePrivateHistConfiguration : IEntityTypeConfiguration<ViewedArticlePrivateHist>
    {
        public void Configure(EntityTypeBuilder<ViewedArticlePrivateHist> builder)
        {
            //Table name
            builder.ToTable("ViewedArticlePrivateHist");

            //Primary key
            builder.HasKey(key => key.ArticleId);

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .ValueGeneratedNever();

            builder.Property(p => p.ViewTime)
             .HasColumnType("timestamp without time zone")
             .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Article)
                .WithOne()
                .HasForeignKey<ViewedArticlePrivateHist>(p => p.ArticleId)
                .HasConstraintName("FK_Article_ViewedArticlePrivateHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PrivateHistory)
                .WithMany(p => p.ViewedArticles)
                .HasForeignKey(p => p.PrivateHistoryId)
                .HasConstraintName("FK_PrivateHistory_ViewedArticlePrivateHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
