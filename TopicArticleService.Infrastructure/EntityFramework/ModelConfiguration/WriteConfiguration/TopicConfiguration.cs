using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            //Table name
            builder.ToTable("Topic");

            //Private key
            builder.HasKey(key => key.Id);

            var nameConverter = new ValueConverter<TopicName, string>(x => x.Value, x => new TopicName(x));

            var parentTopicIdConverter = new ValueConverter<TopicId, Guid>(x => x.Value, x => new TopicId(x));

            //Property config - Start

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, id => new TopicId(id))
                .HasColumnName("TopicId");

            builder.Property(typeof(TopicName), "_topicName")
                .HasConversion(nameConverter)
                .HasColumnName("TopicName")
                .IsRequired();

            builder.Property(typeof(TopicId), "_parentTopicId")
                .HasConversion(parentTopicIdConverter)
                .HasColumnName("ParentTopicId");

            //Property config - End

            //Relationships
            builder.HasMany(p => p.TopicChildren);
        }
    }
}
