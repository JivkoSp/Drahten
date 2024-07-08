using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a DateTimeOffset into a string, encrypts that string, and then provides the functionality
    //to decrypt it back into a DateTimeOffset.
    internal sealed class EncryptedDateTimeOffsetConverter : ValueConverter<DateTimeOffset, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        public EncryptedDateTimeOffsetConverter(IEncryptionProvider encryptionProvider)
                    //Encrypts the DateTimeOffset.
            : base(v => encryptionProvider.Encrypt(v.ToString("o")),
               // Decrypts the encrypted string representation of the DateTimeOffset back to DateTimeOffset.
               v => DateTimeOffset.Parse(encryptionProvider.Decrypt(v)))
        {
        }
    }
}
