using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a SearchedData value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a SearchedData value object.
    internal sealed class EncryptedSearchedDataConverter : ValueConverter<SearchedData, string>
    {
        // An instance of IEncryptionProvider used for encryption and decryption operations.
        private readonly IEncryptionProvider _encryptionProvider;

        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedSearchedDataConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider), // Encrypts the SearchedData value object.
                  v => ConvertToSearchedData(v, encryptionProvider), // Decrypts the encrypted string representation of the SearchedData value object to SearchedData value object.
                  mappingHints)
        {
            _encryptionProvider = encryptionProvider;
        }

        #region DESCRIPTION

        // Converts an SearchedData value object to its encrypted string representation.
        /// <param name="comment">The SearchedData value object to convert and encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the SearchedData value object.</returns>

        #endregion
        private static string ConvertToString(string searchedData, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string representation of the SearchedData.
            string encryptedValue = encryptionProvider.Encrypt(searchedData);

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of the SearchedData value object to SearchedData value object.
        /// <param name="value">The encrypted string representation of the SearchedData value object to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted SearchedData value object.</returns>

        #endregion
        private static SearchedData ConvertToSearchedData(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the SearchedData value object.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert the decrypted string back to an SearchedData value object.
            return new SearchedData(decryptedValue);
        }
    }
}
