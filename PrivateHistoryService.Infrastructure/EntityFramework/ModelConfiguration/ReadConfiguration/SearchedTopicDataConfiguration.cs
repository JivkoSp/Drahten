using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class SearchedTopicDataConfiguration : IEntityTypeConfiguration<SearchedTopicDataReadModel>
    {
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
                .IsRequired();

            builder.Property(p => p.DateTime)
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
