using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedStringConverter : ValueConverter<string, string>
    {
        public EncryptedStringConverter(IEncryptionProvider encryptionProvider)
            : base(v => encryptionProvider.Encrypt(v.ToString()),
                   v => encryptionProvider.Decrypt(v))
        {
        }
    }
}
