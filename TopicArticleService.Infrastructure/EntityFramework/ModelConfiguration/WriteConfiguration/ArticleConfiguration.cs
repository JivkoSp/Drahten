using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            //Table name
            builder.ToTable("Article");

            //Primary key
            builder.HasKey(key => key.Id);

            var prevTitleConverter = new ValueConverter<ArticlePrevTitle, string>(x => x.Value, x => new ArticlePrevTitle(x));

            var titleConverter = new ValueConverter<ArticleTitle, string>(x => x.Value, x => new ArticleTitle(x));

            var contentConverter = new ValueConverter<ArticleContent, string>(x => x.Value, x => new ArticleContent(x));

            var publishingDateConverter = new ValueConverter<ArticlePublishingDate, string>(x => x.Value, x => new ArticlePublishingDate(x));

            var authorConverter = new ValueConverter<ArticleAuthor, string>(x => x.Value, x => new ArticleAuthor(x));

            var linkConverter = new ValueConverter<ArticleLink, string>(x => x.Value, x => new ArticleLink(x));

            var topicIdConverter = new ValueConverter<TopicId, Guid>(x => x.Value, x => new TopicId(x));

            //Property config - Start

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value.ToString(), id => new ArticleID(Guid.Parse(id)))
                .HasColumnName("ArticleId");

            builder.Property(typeof(ArticlePrevTitle), "_prevTitle")
                .HasConversion(prevTitleConverter)
                .HasColumnName("PrevTitle")
                .IsRequired();

            builder.Property(typeof(ArticleTitle), "_title")
                .HasConversion(titleConverter)
                .HasColumnName("Title")
                .IsRequired();

            builder.Property(typeof(ArticleContent), "_content")
                .HasConversion(contentConverter)
                .HasColumnName ("Content")
                .IsRequired();

            builder.Property(typeof(ArticlePublishingDate), "_publishingDate")
                .HasConversion(publishingDateConverter)
                .HasColumnName("PublishingDate")
                .IsRequired();

            builder.Property(typeof(ArticleAuthor), "_author")
                .HasConversion(authorConverter)
                .HasColumnName("Author")
                .IsRequired();

            builder.Property(typeof(ArticleLink), "_link")
                .HasConversion(linkConverter)
                .HasColumnName("Link")
                .IsRequired();

            builder.Property(typeof(TopicId), "_topicId")
                .HasConversion(topicIdConverter)
                .HasColumnName("TopicId")
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasMany(p => p.UserArticles);

            builder.HasMany(p => p.ArticleLikes);

            builder.HasMany(p => p.ArticleDislikes);

            builder.HasMany(p => p.ArticleComments);
        }
    }
}
