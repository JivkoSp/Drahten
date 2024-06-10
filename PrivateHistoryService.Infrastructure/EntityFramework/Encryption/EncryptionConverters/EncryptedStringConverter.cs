using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedStringConverter<T> : ValueConverter<T, string> where T : class
    {
        public EncryptedStringConverter(IEncryptionProvider encryptionProvider)
            : base(v => encryptionProvider.Encrypt(v.ToString()),
                   v => (T)Convert.ChangeType(encryptionProvider.Decrypt(v), typeof(T)))
        {
        }
    }
}
