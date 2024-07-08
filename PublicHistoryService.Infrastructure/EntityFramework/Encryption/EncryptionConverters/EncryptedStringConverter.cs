using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Converts a string into a encrypted string and then provides the functionality to decrypt it back.
    internal sealed class EncryptedStringConverter : ValueConverter<string, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        public EncryptedStringConverter(IEncryptionProvider encryptionProvider)
                   //Encrypts the string.
            : base(v => encryptionProvider.Encrypt(v.ToString()),
                   // Decrypts the encrypted string.
                   v => encryptionProvider.Decrypt(v))
        {
        }
    }
}
