using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedUserRetentionUntilConverter : ValueConverter<UserRetentionUntil, string>
    {
        public EncryptedUserRetentionUntilConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToDateTimeOffset(v, encryptionProvider),
                  v => ConvertToUserRetentionUntil(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToDateTimeOffset(DateTimeOffset dateTime, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the DateTime
            string encryptedValue = encryptionProvider.Encrypt(dateTime.ToString("o"));

            return encryptedValue;
        }

        private static UserRetentionUntil ConvertToUserRetentionUntil(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to ArticleComment
            return new UserRetentionUntil(DateTimeOffset.Parse(decryptedValue));
        }
    }
}
