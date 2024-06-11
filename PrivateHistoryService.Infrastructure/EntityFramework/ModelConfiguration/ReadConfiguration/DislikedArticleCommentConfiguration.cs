using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class DislikedArticleCommentConfiguration : IEntityTypeConfiguration<DislikedArticleCommentReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public DislikedArticleCommentConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<DislikedArticleCommentReadModel> builder)
        {
            //Table name
            builder.ToTable("DislikedArticleComment");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentId, key.UserId });

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.DislikedArticleComments)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_DislikedArticleComments")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
