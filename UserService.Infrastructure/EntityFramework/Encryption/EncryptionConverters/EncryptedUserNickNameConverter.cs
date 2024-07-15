using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a UserNickName value object into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a UserNickName value object.
    internal sealed class EncryptedUserNickNameConverter : ValueConverter<UserNickName, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        /// <param name="mappingHints">Optional mapping hints for the value converter.</param>
        public EncryptedUserNickNameConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider), // Encrypts the UserNickName value object.
                  v => ConvertToUserNickName(v, encryptionProvider), // Decrypts the encrypted string representation of the UserNickName value object to UserNickName value object.
                  mappingHints)
        {
        }

        #region DESCRIPTION

        // Converts an string value to its encrypted representation.
        /// <param name="userNickName">The userNickName value to encrypt.</param>
        /// <param name="encryptionProvider">The encryption provider used to encrypt the string.</param>
        /// <returns>The encrypted string representation of the userNickName value.</returns>

        #endregion
        private static string ConvertToString(string userNickName, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string representation of the UserNickName value object.
            string encryptedValue = encryptionProvider.Encrypt(userNickName);

            return encryptedValue;
        }

        #region DESCRIPTION

        // Converts an encrypted string representation of userNickName value to UserNickName value object.
        /// <param name="value">The encrypted string representation of the userNickName value to decrypt and convert.</param>
        /// <param name="encryptionProvider">The encryption provider used to decrypt the string.</param>
        /// <returns>The decrypted UserNickName value object.</returns>

        #endregion
        private static UserNickName ConvertToUserNickName(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string representation of the userNickName value.
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert the string to UserNickName value object.
            return new UserNickName(decryptedValue);
        }
    }
}
