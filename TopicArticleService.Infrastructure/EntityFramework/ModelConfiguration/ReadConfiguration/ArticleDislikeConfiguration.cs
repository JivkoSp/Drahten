using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ArticleDislikeConfiguration : IEntityTypeConfiguration<ArticleDislikeReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ArticleDislikeConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ArticleDislikeReadModel> builder)
        {
            //Table name
            builder.ToTable("ArticleDislike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleId, key.UserId });

            //Property config
            builder.Property(p => p.DateTime)
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.ArticleDislikes)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_ArticleDislikes")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleDislikes)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleDislikes")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
