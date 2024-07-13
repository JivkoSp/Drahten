using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a ArticleCommentValue value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a ArticleCommentValue value object.
    internal sealed class EncryptedArticleCommentValueConverter : ValueConverter<ArticleCommentValue, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedArticleCommentValueConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider), // Encrypts the ArticleCommentValue value object.
                  v => ConvertToArticleCommentValue(v, encryptionProvider), // Decrypts the encrypted string representation of the ArticleCommentValue value object to ArticleCommentValue value object.
                  mappingHints)
        {
        }

        #region DESCRIPTION

        // Converts an string value to its encrypted representation.
        /// <param name="comment">The comment value to convert and encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the comment value.</returns>

        #endregion
        private static string ConvertToString(string comment, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string representation of the ArticleCommentValue value object.
            string encryptedValue = encryptionProvider.Encrypt(comment);

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of comment value to ArticleCommentValue value object.
        /// <param name="value">The encrypted string representation of the comment value to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted ArticleCommentValue value object.</returns>

        #endregion
        private static ArticleCommentValue ConvertToArticleCommentValue(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the comment value.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to ArticleCommentValue value object.
            return new ArticleCommentValue(decryptedValue);
        }
    }
}
