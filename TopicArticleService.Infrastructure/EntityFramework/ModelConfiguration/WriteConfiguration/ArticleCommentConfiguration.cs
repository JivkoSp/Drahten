using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleCommentConfiguration : IEntityTypeConfiguration<ArticleComment>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ArticleCommentConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ArticleComment> builder)
        {
            //Table name
            builder.ToTable("ArticleComment");

            //Primary key
            builder.HasKey(key => key.Id);

            //Property config - Start

            //Shadow property for ArticleId.
            builder.Property<ArticleID>("ArticleId")
                .HasConversion(id => id.Value.ToString("N"), id => new ArticleID(Guid.ParseExact(id, "N")));

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value, id => new ArticleCommentID(id))
                .HasColumnName("ArticleCommentId");

            builder.Property(typeof(ArticleCommentValue), "_commentValue")
                .HasConversion(new EncryptedArticleCommentValueConverter(_encryptionProvider))
                .HasColumnName("Comment")
                .IsRequired();

            builder.Property(typeof(ArticleCommentDateTime), "_dateTime")
                .HasConversion(new EncryptedArticleCommentDateTimeConverter(_encryptionProvider))
                .HasColumnName("DateTime")
                .IsRequired();

            builder.Property(typeof(UserID), "_userId")
                .HasConversion(new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x))))
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(typeof(ArticleCommentID), "_parentArticleCommentId")
                .HasConversion(new ValueConverter<ArticleCommentID, Guid>(x => x.Value, x => new ArticleCommentID(x)))
                .HasColumnName("ParentArticleCommentId");

            //Property config - End

            //Relationships
            builder.HasMany(p => p.ArticleCommentLikes);

            builder.HasMany(p => p.ArticleCommentDislikes);
        }
    }
}
