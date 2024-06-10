using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
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
