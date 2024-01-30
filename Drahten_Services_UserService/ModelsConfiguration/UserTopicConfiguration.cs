using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class UserTopicConfiguration : IEntityTypeConfiguration<UserTopic>
    {
        public void Configure(EntityTypeBuilder<UserTopic> builder)
        {
            //Table name
            builder.ToTable("UserTopic");

            //Primary key
            builder.HasKey(key => new { key.UserId, key.TopicId });

            //Property config
            builder.Property(p => p.SubscriptionTime)
             .HasColumnType("timestamp without time zone")
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
