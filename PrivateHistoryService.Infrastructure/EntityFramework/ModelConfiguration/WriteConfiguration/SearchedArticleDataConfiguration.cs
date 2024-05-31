using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class SearchedArticleDataConfiguration : IEntityTypeConfiguration<SearchedArticleData>
    {
        public void Configure(EntityTypeBuilder<SearchedArticleData> builder)
        {
            //Table name
            builder.ToTable("SearchedArticleData");

            //Shadow property for SearchedArticleDataId.
            builder.Property<Guid>("SearchedArticleDataId");

            //Primary key
            builder.HasKey("SearchedArticleDataId");

            //Property config - Start

            var searchedDataConverter = new ValueConverter<SearchedData, string>(x => x, x => new SearchedData(x));

            #region ConversionToUUID
            // *** IMPORTANT! ***
            // Convert Guid to string without hyphens.
            // Explanation: UUIDs are commonly represented as a 32-character hexadecimal string (such as "37955c6317e6869e90fe0029564078c8")
            // or as a 36-character string with hyphens separating groups of characters (such as "37955c63-17e6-869e-90fe-0029564078c8").
            // The Id is NOT stored as Guid in the database, because PostgreSQL will automatically format it with hyphens for readability
            // and standardization. This will cause problems when accessing documents with their Id's from the SearchService database store
            // (a Elasticserach instance). The Elasticsearch instance will expect the Id's to be in a 32-character hexadecimal format and 
            // will not be able to find the document's if it receaves a 36-character hexadecimal format for the Id.
            // ---------------------------------------
            // !!! The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            #endregion
            builder.Property(p => p.ArticleID)
                .HasConversion(id => id.Value.ToString("N"), id => new ArticleID(Guid.ParseExact(id, "N")))
                .HasColumnName("ArticleId")
                .IsRequired();

            builder.Property(p => p.UserID)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(typeof(SearchedData), "SearchedData")
                .HasConversion(searchedDataConverter)
                .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
              .IsRequired();

            //Property config - End
        }
    }
}
