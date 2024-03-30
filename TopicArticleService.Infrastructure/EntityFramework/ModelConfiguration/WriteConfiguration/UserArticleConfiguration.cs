using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserArticleConfiguration : IEntityTypeConfiguration<UserArticle>
    {
        public void Configure(EntityTypeBuilder<UserArticle> builder)
        {
            //Table name
            builder.ToTable("UserArticle");

            //Composite primary key
            builder.HasKey(key => new { key.UserID, key.ArticleId });

            var userIdConverter = new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x)));

            var articleIdConverter = new ValueConverter<ArticleID, string>(x => x.Value.ToString(), x => new ArticleID(Guid.Parse(x)));

            //Property config - Start

            builder.Property(p => p.UserID)
                .HasConversion(userIdConverter)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(p => p.ArticleId)
                .HasConversion(articleIdConverter)
                .HasColumnName("ArticleId")
                .IsRequired();

            //Property config - End
        }
    }
}
