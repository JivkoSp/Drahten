using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class SearchedTopicDataConfiguration : IEntityTypeConfiguration<SearchedTopicDataReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public SearchedTopicDataConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<SearchedTopicDataReadModel> builder)
        {
            //Table name
            builder.ToTable("SearchedTopicData");

            //Primary key
            builder.HasKey(key => key.SearchedTopicDataId);

            //Property config - Start

            builder.Property(p => p.TopicId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.SearchedData)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.SearchedTopics)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_SearchedTopics")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
