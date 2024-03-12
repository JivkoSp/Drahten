using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Xml.Linq;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class SearchedArticleDataPublicHistConfiguration : IEntityTypeConfiguration<SearchedArticleDataPublicHist>
    {
        public void Configure(EntityTypeBuilder<SearchedArticleDataPublicHist> builder)
        {
            //Table name
            builder.ToTable("SearchedArticleDataPublicHist");

            //Primary key
            builder.HasKey(key => key.SearchedArticleDataPublicHistId);

            //Property config - Start

            builder.Property(p => p.SearchedData)
                .IsRequired();

            builder.Property(p => p.SearchTime)
              .HasColumnType("timestamp without time zone")
              .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.SearchedArticleDataPublicHist)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_SearchedArticleDataPublicHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PublicHistory)
                .WithMany(p => p.SearchedArticleData)
                .HasForeignKey(p => p.PublicHistoryId)
                .HasConstraintName("FK_PublicHistory_SearchedArticleDataPublicHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
