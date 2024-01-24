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

            //Property config
            builder.Property(p => p.ArticleData)
                .IsRequired();

            //Relationships
            builder.HasOne(p => p.Topic)
                .WithOne()
                .HasForeignKey<Article>(p => p.TopicId)
                .HasConstraintName("FK_Topic_Article")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
