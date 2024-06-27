using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
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
