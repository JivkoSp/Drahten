using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ArticleConfiguration : IEntityTypeConfiguration<ArticleReadModel>
    {
        public void Configure(EntityTypeBuilder<ArticleReadModel> builder)
        {
            //Table name
            builder.ToTable("Article");

            //Primary key
            builder.HasKey(key => key.ArticleId);

            //Property config - Start

            builder.Property(p => p.PrevTitle)
                .IsRequired();

            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.PublishingDate)
                .IsRequired();

            builder.Property(p => p.Author)
                .IsRequired();

            builder.Property(p => p.Link)
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.Topic)
                .WithMany(p => p.Articles)
                .HasForeignKey(p => p.TopicId)
                .HasConstraintName("FK_Topic_Articles")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
