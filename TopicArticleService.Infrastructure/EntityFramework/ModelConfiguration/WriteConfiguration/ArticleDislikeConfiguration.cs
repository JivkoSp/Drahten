using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

            var articleIdConverter = new ValueConverter<ArticleID, string>(x => x.Value.ToString(), x => new ArticleID(Guid.Parse(x)));

            var userIdConverter = new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x)));

            //Property config - Start

            builder.Property(p => p.ArticleID)
                .HasConversion(articleIdConverter)
                .HasColumnName("ArticleId");

            builder.Property(p => p.UserID)
               .HasConversion(userIdConverter)
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(p => p.DateTime)
             .HasColumnType("timestamp without time zone")
             .IsRequired();

            //Property config - End
        }
    }
}
