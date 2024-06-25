using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedDateTimeOffsetConverter : ValueConverter<DateTimeOffset, string>
    {
        public EncryptedDateTimeOffsetConverter(IEncryptionProvider encryptionProvider)
            : base(v => encryptionProvider.Encrypt(v.ToString("o")),
               v => DateTimeOffset.Parse(encryptionProvider.Decrypt(v)))
        {
        }
    }
}
