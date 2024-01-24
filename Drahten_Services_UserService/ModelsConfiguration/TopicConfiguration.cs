using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            //Table name
            builder.ToTable("Topic");

            //Private key
            builder.HasKey(key => key.TopicId);

            //Property config
            builder.Property(p => p.TopicName)
                .IsRequired();
        }
    }
}
