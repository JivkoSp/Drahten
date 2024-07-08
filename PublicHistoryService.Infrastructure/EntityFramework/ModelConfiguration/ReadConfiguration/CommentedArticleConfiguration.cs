using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class CommentedArticleConfiguration : IEntityTypeConfiguration<CommentedArticleReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public CommentedArticleConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<CommentedArticleReadModel> builder)
        {
            //Table name
            builder.ToTable("CommentedArticle");

            //Primary key
            builder.HasKey(key => key.CommentedArticleId);

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.ArticleComment)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.CommentedArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_CommentedArticles")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
