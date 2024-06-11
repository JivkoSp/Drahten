using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ViewedArticleConfiguration : IEntityTypeConfiguration<ViewedArticleReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ViewedArticleConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ViewedArticleReadModel> builder)
        {
            //Table name
            builder.ToTable("ViewedArticle");

            //Primary key
            builder.HasKey(key => key.ViewedArticleId);

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
                .WithMany(p => p.ViewedArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ViewedArticles")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
