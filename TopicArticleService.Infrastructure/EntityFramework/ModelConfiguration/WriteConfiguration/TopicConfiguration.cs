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

            //Property config - Start

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, id => new TopicId(id))
                .HasColumnName("TopicId");

            builder.Property(typeof(TopicName), "_topicName")
                .HasConversion(new ValueConverter<TopicName, string>(x => x, x => new TopicName(x)))
                .HasColumnName("TopicName")
                .IsRequired();

            builder.Property(typeof(TopicFullName), "_topicFullName")
               .HasConversion(new ValueConverter<TopicFullName, string>(x => x, x => new TopicFullName(x)))
               .HasColumnName("TopicFullName")
               .IsRequired();

            builder.Property(typeof(TopicId), "_parentTopicId")
                .HasConversion(new ValueConverter<TopicId, Guid>(x => x.Value, x => new TopicId(x)))
                .HasColumnName("ParentTopicId");

            //Property config - End

            //Relationships
            builder.HasMany(p => p.TopicChildren);
        }
    }
}
