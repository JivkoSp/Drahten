using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleDislikeConfiguration : IEntityTypeConfiguration<ArticleDislike>
    {
        public void Configure(EntityTypeBuilder<ArticleDislike> builder)
        {
            //Table name
            builder.ToTable("ArticleDislike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleID, key.UserID });

            //Property config - Start

            builder.Property(p => p.ArticleID)
                .HasConversion(id => id.Value.ToString("N"), id => new ArticleID(Guid.ParseExact(id, "N")))
                .HasColumnName("ArticleId");

            builder.Property(p => p.UserID)
               .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(p => p.DateTime)
             .IsRequired();

            //Property config - End
        }
    }
}
