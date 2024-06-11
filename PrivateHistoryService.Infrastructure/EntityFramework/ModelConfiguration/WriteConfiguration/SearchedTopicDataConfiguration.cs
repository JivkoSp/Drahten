using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class SearchedTopicDataConfiguration : IEntityTypeConfiguration<SearchedTopicData>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public SearchedTopicDataConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<SearchedTopicData> builder)
        {
            //Table name
            builder.ToTable("SearchedTopicData");

            //Shadow property for SearchedTopicDataId.
            builder.Property<Guid>("SearchedTopicDataId");

            //Primary key
            builder.HasKey("SearchedTopicDataId");

            //Property config - Start

            builder.Property(p => p.TopicID)
                .HasConversion(id => id.Value, id => new TopicID(id))
                .HasColumnName("TopicId")
                .IsRequired();

            builder.Property(p => p.UserID)
               .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(typeof(SearchedData), "SearchedData")
                .HasConversion(new EncryptedSearchedDataConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End
        }
    }
}
