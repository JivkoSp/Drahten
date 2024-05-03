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

            #region ConversionToUUID
            // *** IMPORTANT! ***
            // Convert Guid to string without hyphens.
            // Explanation: UUIDs are commonly represented as a 32-character hexadecimal string (such as "37955c6317e6869e90fe0029564078c8")
            // or as a 36-character string with hyphens separating groups of characters (such as "37955c63-17e6-869e-90fe-0029564078c8").
            // The Id is NOT stored as Guid in the database, because PostgreSQL will automatically format it with hyphens for readability
            // and standardization. This will cause problems when accessing documents with their Id's from the SearchService database store
            // (a Elasticserach instance). The Elasticsearch instance will expect the Id's to be in a 32-character hexadecimal format and 
            // will not be able to find the document's if it receaves a 36-character hexadecimal format for the Id.
            // ---------------------------------------
            // !!! The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            #endregion
            builder.Property(p => p.Id)
                .HasConversion(id => id.Value.ToString("N"), id => new ArticleID(Guid.ParseExact(id, "N"))) 
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
