using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class SearchedArticleDataPrivateHistConfiguration : IEntityTypeConfiguration<SearchedArticleDataPrivateHist>
    {
        public void Configure(EntityTypeBuilder<SearchedArticleDataPrivateHist> builder)
        {
            //Table name
            builder.ToTable("SearchedArticleDataPrivateHist");

            //Primary key
            builder.HasKey(key => key.SearchedArticleDataPrivateHistId);

            //Property config - Start

            builder.Property(p => p.SearchedData)
                .IsRequired();

            builder.Property(p => p.SearchTime)
              .HasColumnType("timestamp without time zone")
              .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.SearchedArticleDataPrivateHist)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_SearchedArticleDataPrivateHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PrivateHistory)
                .WithMany(p => p.SearchedArticleData)
                .HasForeignKey(p => p.PrivateHistoryId)
                .HasConstraintName("FK_PrivateHistory_SearchedArticleDataPrivateHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
