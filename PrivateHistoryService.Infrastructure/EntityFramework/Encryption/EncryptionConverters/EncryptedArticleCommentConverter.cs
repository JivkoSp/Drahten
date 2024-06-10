using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedArticleCommentConverter : ValueConverter<ArticleComment, string>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public EncryptedArticleCommentConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToArticleComment(v, encryptionProvider),
                  mappingHints)
        {
            _encryptionProvider = encryptionProvider;
        }

        private static string ConvertToString(string comment, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(comment);

            return encryptedValue;
        }

        private static ArticleComment ConvertToArticleComment(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to ArticleComment
            return new ArticleComment(decryptedValue);
        }
    }
}
