using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class SearchedArticleDataConfiguration : IEntityTypeConfiguration<SearchedArticleDataReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public SearchedArticleDataConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<SearchedArticleDataReadModel> builder)
        {
            //Table name
            builder.ToTable("SearchedArticleData");

            //Primary key
            builder.HasKey(key => key.SearchedArticleDataId);

            //Property config - Start

            builder.Property(p => p.ArticleId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.Property(p => p.SearchedData)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.SearchedArticles)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_SearchedArticles")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
