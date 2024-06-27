using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedUserEmailAddressConverter : ValueConverter<UserEmailAddress, string>
    {
        public EncryptedUserEmailAddressConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToUserEmailAddress(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToString(string userEmailAddress, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(userEmailAddress);

            return encryptedValue;
        }

        private static UserEmailAddress ConvertToUserEmailAddress(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to UserEmailAddress
            return new UserEmailAddress(decryptedValue);
        }
    }
}
