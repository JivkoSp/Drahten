using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

            //Property config - Start

            builder.Property(p => p.UserID)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(p => p.ArticleId)
                .HasConversion(id => id.Value.ToString("N"), id => new ArticleID(Guid.ParseExact(id, "N")))
                .HasColumnName("ArticleId")
                .IsRequired();

            //Property config - End
        }
    }
}
