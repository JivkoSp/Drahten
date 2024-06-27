using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedUserNickNameConverter : ValueConverter<UserNickName, string>
    {
        public EncryptedUserNickNameConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToUserNickName(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToString(string userNickName, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(userNickName);

            return encryptedValue;
        }

        private static UserNickName ConvertToUserNickName(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to UserNickName
            return new UserNickName(decryptedValue);
        }
    }
}
