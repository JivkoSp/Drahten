using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedUserFullNameConverter : ValueConverter<UserFullName, string>
    {
        public EncryptedUserFullNameConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToUserFullName(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToString(string userFullName, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(userFullName);

            return encryptedValue;
        }

        private static UserFullName ConvertToUserFullName(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to UserFullName
            return new UserFullName(decryptedValue);
        }
    }
}
