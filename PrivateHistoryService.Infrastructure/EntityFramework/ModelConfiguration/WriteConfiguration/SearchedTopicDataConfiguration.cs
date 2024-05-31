using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class SearchedTopicDataConfiguration : IEntityTypeConfiguration<SearchedTopicData>
    {
        public void Configure(EntityTypeBuilder<SearchedTopicData> builder)
        {
            //Table name
            builder.ToTable("SearchedTopicData");

            //Shadow property for SearchedTopicDataId.
            builder.Property<Guid>("SearchedTopicDataId");

            //Primary key
            builder.HasKey("SearchedTopicDataId");

            //Property config - Start

            var searchedDataConverter = new ValueConverter<SearchedData, string>(x => x, x => new SearchedData(x));

            builder.Property(p => p.TopicID)
                .HasConversion(id => id.Value, id => new TopicID(id))
                .HasColumnName("TopicId")
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
