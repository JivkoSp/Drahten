using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a ArticleComment value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a ArticleComment value object.
    internal sealed class EncryptedArticleCommentConverter : ValueConverter<ArticleComment, string>
    {
        // An instance of IEncryptionProvider used for encryption and decryption operations.
        private readonly IEncryptionProvider _encryptionProvider;

        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedArticleCommentConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),  // Encrypts the ArticleComment value object.
                  v => ConvertToArticleComment(v, encryptionProvider), // Decrypts the encrypted string representation of the ArticleComment value object to ArticleComment value object.
                  mappingHints)
        {
            _encryptionProvider = encryptionProvider;
        }

        #region DESCRIPTION

        // Converts an ArticleComment value object to its encrypted string representation.
        /// <param name="comment">The ArticleComment value object to convert and encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the ArticleComment value object.</returns>

        #endregion
        private static string ConvertToString(string comment, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string representation of the ArticleComment.
            string encryptedValue = encryptionProvider.Encrypt(comment);

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of the ArticleComment value object to ArticleComment value object.
        /// <param name="value">The encrypted string representation of the ArticleComment value object to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted ArticleComment value object.</returns>

        #endregion
        private static ArticleComment ConvertToArticleComment(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the ArticleComment value object.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert the decrypted string back to an ArticleComment.
            return new ArticleComment(decryptedValue);
        }
    }
}
