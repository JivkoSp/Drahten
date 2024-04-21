using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserTopicConfiguration : IEntityTypeConfiguration<UserTopic>
    {
        public void Configure(EntityTypeBuilder<UserTopic> builder)
        {
            //Table name
            builder.ToTable("UserTopic");

            //Primary key
            builder.HasKey(key => new { key.UserId, key.TopicId });

            //Property config
            builder.Property(p => p.SubscriptionTime)
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
