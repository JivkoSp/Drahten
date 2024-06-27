using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedArticleCommentDateTimeConverter : ValueConverter<ArticleCommentDateTime, string>
    {
        public EncryptedArticleCommentDateTimeConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
             : base(
                  v => ConvertToDateTimeOffset(v, encryptionProvider),
                  v => ConvertToArticleCommentDateTime(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToDateTimeOffset(DateTimeOffset dateTime, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the DateTime
            string encryptedValue = encryptionProvider.Encrypt(dateTime.ToString("o"));

            return encryptedValue;
        }

        private static ArticleCommentDateTime ConvertToArticleCommentDateTime(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to ArticleComment
            return new ArticleCommentDateTime(DateTimeOffset.Parse(decryptedValue));
        }
    }
}
