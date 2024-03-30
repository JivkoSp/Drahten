using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class TopicConfiguration : IEntityTypeConfiguration<TopicReadModel>
    {
        public void Configure(EntityTypeBuilder<TopicReadModel> builder)
        {
            //Table name
            builder.ToTable("Topic");

            //Private key
            builder.HasKey(key => key.TopicId);

            //Property config
            builder.Property(p => p.TopicName)
                .IsRequired();

            //Relationships
            builder.HasOne(p => p.Parent)
                .WithMany(p => p.Children)
                .HasForeignKey(p => p.ParentTopicId)
                .HasConstraintName("FK_ParentTopic_ChildTopics")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
