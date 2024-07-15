using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a DateTimeOffset value into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a DateTimeOffset value.
    internal sealed class EncryptedDateTimeOffsetConverter : ValueConverter<DateTimeOffset, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        public EncryptedDateTimeOffsetConverter(IEncryptionProvider encryptionProvider)
            : base(v => encryptionProvider.Encrypt(v.ToString("o")),
               v => DateTimeOffset.Parse(encryptionProvider.Decrypt(v)))
        {
        }
    }
}
