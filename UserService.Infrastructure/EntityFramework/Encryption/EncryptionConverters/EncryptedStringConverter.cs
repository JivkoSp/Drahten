using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    //Encrypts a string value and then provides the functionality to decrypt it back.
    internal sealed class EncryptedStringConverter : ValueConverter<string, string>
    {
        /// <param name="encryptionProvider">An instance of IEncryptionProvider used for encryption and decryption operations.</param>
        public EncryptedStringConverter(IEncryptionProvider encryptionProvider)
            : base(v => encryptionProvider.Encrypt(v.ToString()),
                   v => encryptionProvider.Decrypt(v))
        {
        }
    }
}
