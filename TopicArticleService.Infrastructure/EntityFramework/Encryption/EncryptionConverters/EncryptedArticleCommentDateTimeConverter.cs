using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Domain.ValueObjects;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a ArticleCommentDateTime value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a ArticleCommentDateTime value object.
    internal sealed class EncryptedArticleCommentDateTimeConverter : ValueConverter<ArticleCommentDateTime, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedArticleCommentDateTimeConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
             : base(
                  v => ConvertToDateTimeOffset(v, encryptionProvider), // Encrypts the ArticleCommentDateTime value object.
                  v => ConvertToArticleCommentDateTime(v, encryptionProvider), // Decrypts the encrypted string representation of the ArticleCommentDateTime value object to ArticleCommentDateTime value object.
                  mappingHints)
        {
        }

        #region DESCRIPTION

        // Converts an DateTimeOffset value to its encrypted string representation.
        /// <param name="dateTime">The DateTimeOffset value to convert and encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the DateTimeOffset value.</returns>

        #endregion
        private static string ConvertToDateTimeOffset(DateTimeOffset dateTime, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the DateTimeOffset representation of the ArticleCommentDateTime value object.
            string encryptedValue = encryptionProvider.Encrypt(dateTime.ToString("o"));

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of DateTimeOffset value to ArticleCommentDateTime value object.
        /// <param name="value">The encrypted string representation of the DateTimeOffset value to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted ArticleCommentDateTime value object.</returns>

        #endregion
        private static ArticleCommentDateTime ConvertToArticleCommentDateTime(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the DateTimeOffset value.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to ArticleCommentDateTime value object.
            return new ArticleCommentDateTime(DateTimeOffset.Parse(decryptedValue));
        }
    }
}
