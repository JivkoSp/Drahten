using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ArticleCommentLikeConfiguration : IEntityTypeConfiguration<ArticleCommentLikeReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ArticleCommentLikeConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ArticleCommentLikeReadModel> builder)
        {
            //Table name
            builder.ToTable("ArticleCommentLike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentId, key.UserId });
            
            //Property config
            builder.Property(p => p.DateTime)
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleCommentLikes)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleCommentLikes")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.ArticleComment)
                .WithMany(p => p.ArticleCommentLikes)
                .HasForeignKey(p => p.ArticleCommentId)
                .HasConstraintName("FK_ArticleComment_ArticleCommentLikes")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
