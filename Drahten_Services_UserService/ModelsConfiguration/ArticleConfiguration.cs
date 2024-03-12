using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //Table name
            builder.ToTable("Article");

            //Primary key
            builder.HasKey(key => key.ArticleId);

            //Property config - START

            builder.Property(p => p.PrevTitle)
                .IsRequired();

            builder.Property(p => p.Title)
                .IsRequired();

            builder.Property(p => p.Data)
                .IsRequired();

            builder.Property(p => p.Date)
                .IsRequired();

            builder.Property(p => p.Author)
                .IsRequired();

            builder.Property(p => p.Link)
                .IsRequired();

            //Property config - END

            //Relationships
            builder.HasOne(p => p.Topic)
                .WithMany(p => p.Articles)
                .HasForeignKey(p => p.TopicId)
                .HasConstraintName("FK_Topic_Article")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
