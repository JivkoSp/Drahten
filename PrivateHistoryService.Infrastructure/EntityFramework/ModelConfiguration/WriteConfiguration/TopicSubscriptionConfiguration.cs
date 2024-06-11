using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class TopicSubscriptionConfiguration : IEntityTypeConfiguration<TopicSubscription>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public TopicSubscriptionConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<TopicSubscription> builder)
        {
            //Table name
            builder.ToTable("TopicSubscription");

            //Composite primary key
            builder.HasKey(key => new { key.TopicID, key.UserID });

            //Property config - Start

            builder.Property(p => p.TopicID)
                .HasConversion(id => id.Value, id => new TopicID(id))
                .HasColumnName("TopicId")
                .IsRequired();

            builder.Property(p => p.UserID)
               .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Property config - End
        }
    }
}
