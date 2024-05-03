using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLike>
    {
        public void Configure(EntityTypeBuilder<ArticleLike> builder)
        {
            //Table name
            builder.ToTable("ArticleLike");

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
