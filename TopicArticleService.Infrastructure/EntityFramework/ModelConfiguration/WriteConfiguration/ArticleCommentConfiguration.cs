using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleCommentConfiguration : IEntityTypeConfiguration<ArticleComment>
    {
        public void Configure(EntityTypeBuilder<ArticleComment> builder)
        {
            //Table name
            builder.ToTable("ArticleComment");

            //Primary key
            builder.HasKey(key => key.Id);

            var commentValueConverter = new ValueConverter<ArticleCommentValue, string>(x => x.Value, x => new ArticleCommentValue(x));

            var commentDateTimeConverter = new ValueConverter<ArticleCommentDateTime, DateTime>(x => x.DateTime, 
                x => new ArticleCommentDateTime(x));

            var userIdConverter = new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x)));

            var parentCommentIdConverter = new ValueConverter<ArticleCommentID, Guid>(x => x.Value, x => new ArticleCommentID(x));

            var articleIdConverter = new ValueConverter<ArticleID, string>(x => x.Value.ToString(), x => new ArticleID(Guid.Parse(x)));

            //Property config - Start

            //Shadow property for ArticleId.
            builder.Property<ArticleID>("ArticleId")
                .HasConversion(articleIdConverter);

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, id => new ArticleCommentID(id))
                .HasColumnName("ArticleCommentId");

            builder.Property(typeof(ArticleCommentValue), "_commentValue")
                .HasConversion(commentValueConverter)
                .HasColumnName("Comment")
                .IsRequired();

            builder.Property(typeof(ArticleCommentDateTime), "_dateTime")
                .HasConversion(commentDateTimeConverter)
                .HasColumnName("DateTime")
                .IsRequired();

            builder.Property(typeof(UserID), "_userId")
                .HasConversion(userIdConverter)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(typeof(ArticleCommentID), "_parentArticleCommentId")
                .HasConversion(parentCommentIdConverter)
                .HasColumnName("ParentArticleCommentId");

            //Property config - End

            //Relationships
            builder.HasMany(p => p.ArticleCommentLikes);

            builder.HasMany(p => p.ArticleCommentDislikes);
        }
    }
}
