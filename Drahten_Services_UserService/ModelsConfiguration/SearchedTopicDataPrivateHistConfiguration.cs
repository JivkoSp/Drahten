using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class SearchedTopicDataPrivateHistConfiguration : IEntityTypeConfiguration<SearchedTopicDataPrivateHist>
    {
        public void Configure(EntityTypeBuilder<SearchedTopicDataPrivateHist> builder)
        {
            //Table name
            builder.ToTable("SearchedTopicDataPrivateHist");

            //Primary key
            builder.HasKey(key => key.SearchedTopicDataPrivateHistId);

            //Property config - Start

            builder.Property(p => p.SearchedData)
                .IsRequired();

            builder.Property(p => p.SearchTime)
              .HasColumnType("timestamp without time zone")
              .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Topic)
                .WithMany(p => p.SearchedTopicDataPrivateHist)
                .HasForeignKey(p => p.TopicId)
                .HasConstraintName("FK_Topic_SearchedTopicDataPrivateHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PrivateHistory)
                .WithMany(p => p.SearchedTopicData)
                .HasForeignKey(p => p.PrivateHistoryId)
                .HasConstraintName("FK_PrivateHistory_SearchedTopicDataPrivateHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
