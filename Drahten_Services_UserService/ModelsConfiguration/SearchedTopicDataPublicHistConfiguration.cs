using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class SearchedTopicDataPublicHistConfiguration : IEntityTypeConfiguration<SearchedTopicDataPublicHist>
    {
        public void Configure(EntityTypeBuilder<SearchedTopicDataPublicHist> builder)
        {
            //Table name
            builder.ToTable("SearchedTopicDataPublicHist");

            //Primary key
            builder.HasKey(key => key.SearchedTopicDataPublicHistId);

            //Property config - Start

            builder.Property(p => p.SearchedData)
                .IsRequired();

            builder.Property(p => p.SearchTime)
              .HasColumnType("timestamp without time zone");

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Topic)
               .WithMany(p => p.SearchedTopicDataPublicHist)
               .HasForeignKey(p => p.TopicId)
               .HasConstraintName("FK_Topic_SearchedTopicDataPublicHist")
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PublicHistory)
                .WithMany(p => p.SearchedTopicData)
                .HasForeignKey(p => p.PublicHistoryId)
                .HasConstraintName("FK_PublicHistory_SearchedTopicDataPublicHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
