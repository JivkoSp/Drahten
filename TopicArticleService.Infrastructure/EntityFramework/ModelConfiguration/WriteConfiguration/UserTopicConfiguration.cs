using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserTopicConfiguration : IEntityTypeConfiguration<UserTopic>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserTopicConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<UserTopic> builder)
        {
            //Table name
            builder.ToTable("UserTopic");

            //Primary key
            builder.HasKey(key => new { key.UserId, key.TopicId });

            //Property config
            builder.Property(typeof(DateTimeOffset), "SubscriptionTime")
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            builder.Property(p => p.UserId)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(p => p.TopicId)
                .HasConversion(id => id.Value, id => new TopicId(id))
                .HasColumnName("TopicId")
                .IsRequired();
        }
    }
}
