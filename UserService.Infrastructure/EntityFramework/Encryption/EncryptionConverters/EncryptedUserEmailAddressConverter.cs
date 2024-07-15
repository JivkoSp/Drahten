using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a UserEmailAddress value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a UserEmailAddress value object.
    internal sealed class EncryptedUserEmailAddressConverter : ValueConverter<UserEmailAddress, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedUserEmailAddressConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider), // Encrypts the UserEmailAddress value object.
                  v => ConvertToUserEmailAddress(v, encryptionProvider), // Decrypts the encrypted string representation of the UserEmailAddress value object to UserEmailAddress value object.
                  mappingHints)
        {
        }

        #region DESCRIPTION

        // Converts an string value to its encrypted representation.
        /// <param name="userEmailAddress">The userEmailAddress value to encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the userEmailAddress value.</returns>

        #endregion
        private static string ConvertToString(string userEmailAddress, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string representation of the UserEmailAddress value object.
            string encryptedValue = encryptionProvider.Encrypt(userEmailAddress);

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of userEmailAddress value to UserEmailAddress value object.
        /// <param name="value">The encrypted string representation of the userEmailAddress value to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted UserEmailAddress value object.</returns>

        #endregion
        private static UserEmailAddress ConvertToUserEmailAddress(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the userEmailAddress value.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert the string to UserEmailAddress value object.
            return new UserEmailAddress(decryptedValue);
        }
    }
}
