using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a UserFullName value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a UserFullName value object.
    internal sealed class EncryptedUserFullNameConverter : ValueConverter<UserFullName, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedUserFullNameConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider), // Encrypts the UserFullName value object.
                  v => ConvertToUserFullName(v, encryptionProvider), // Decrypts the encrypted string representation of the UserFullName value object to UserFullName value object.
                  mappingHints)
        {
        }

        #region DESCRIPTION

        // Converts an string value to its encrypted representation.
        /// <param name="userFullName">The userFullName value to encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the userFullName value.</returns>

        #endregion
        private static string ConvertToString(string userFullName, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string representation of the UserFullName value object.
            string encryptedValue = encryptionProvider.Encrypt(userFullName);

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of userFullName value to UserFullName value object.
        /// <param name="value">The encrypted string representation of the userFullName value to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted UserFullName value object.</returns>

        #endregion
        private static UserFullName ConvertToUserFullName(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the userFullName value.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert the string to UserFullName value object.
            return new UserFullName(decryptedValue);
        }
    }
}
