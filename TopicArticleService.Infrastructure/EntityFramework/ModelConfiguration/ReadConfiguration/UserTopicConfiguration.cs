using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserTopicConfiguration : IEntityTypeConfiguration<UserTopicReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserTopicConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<UserTopicReadModel> builder)
        {
            //Table name
            builder.ToTable("UserTopic");

            //Primary key
            builder.HasKey(key => new { key.UserId, key.TopicId });

            //Property config
            builder.Property(p => p.SubscriptionTime)
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.Topics)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_Topics")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Topic)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.TopicId)
                .HasConstraintName("FK_Topic_Users")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
