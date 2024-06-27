using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleCommentDislikeConfiguration : IEntityTypeConfiguration<ArticleCommentDislike>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ArticleCommentDislikeConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ArticleCommentDislike> builder)
        {
            //Table name
            builder.ToTable("ArticleCommentDislike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentID, key.UserID });

            //Property config - Start

            builder.Property(p => p.ArticleCommentID)
                .HasConversion(new ValueConverter<ArticleCommentID, Guid>(x => x.Value, x => new ArticleCommentID(x)))
                .HasColumnName("ArticleCommentId")
                .IsRequired();

            builder.Property(p => p.UserID)
               .HasConversion(new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x))))
               .HasColumnName("UserId")
               .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End
        }
    }
}
