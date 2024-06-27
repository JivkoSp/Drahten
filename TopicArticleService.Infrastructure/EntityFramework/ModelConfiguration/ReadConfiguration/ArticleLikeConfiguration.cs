using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLikeReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ArticleLikeConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ArticleLikeReadModel> builder)
        {
            //Table name
            builder.ToTable("ArticleLike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleId, key.UserId });

            //Property config
            builder.Property(p => p.DateTime)
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.ArticleLikes)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_ArticleLikes")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleLikes)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleLikes")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
