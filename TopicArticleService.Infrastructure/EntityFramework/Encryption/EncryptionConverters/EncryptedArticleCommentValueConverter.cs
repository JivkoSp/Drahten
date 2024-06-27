using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedArticleCommentValueConverter : ValueConverter<ArticleCommentValue, string>
    {
        public EncryptedArticleCommentValueConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToArticleCommentValue(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToString(string comment, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(comment);

            return encryptedValue;
        }

        private static ArticleCommentValue ConvertToArticleCommentValue(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to ArticleComment
            return new ArticleCommentValue(decryptedValue);
        }
    }
}
