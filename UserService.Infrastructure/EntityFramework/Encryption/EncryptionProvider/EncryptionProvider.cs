using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider
{
    public sealed class EncryptionProvider : IEncryptionProvider
    {
        private readonly byte[] _encryptionKey;

        public EncryptionProvider(string encryptionKey)
        {
            _encryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
        }

        private bool IsBase64String(string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return false;
            }

            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed)
                   && (bytesParsed % 16 == 0);
        }

        public string Encrypt(string plaintext)
        {
            // Generate a random IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                byte[] ivBytes = aesAlg.IV;

                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
                cipher.Init(true, new ParametersWithIV(new KeyParameter(_encryptionKey), ivBytes));

                var inputBytes = Encoding.UTF8.GetBytes(plaintext);
                var outputBytes = cipher.DoFinal(inputBytes);

                // Combine IV and ciphertext (prepend IV to ciphertext)
                byte[] combinedBytes = new byte[ivBytes.Length + outputBytes.Length];
                Buffer.BlockCopy(ivBytes, 0, combinedBytes, 0, ivBytes.Length);
                Buffer.BlockCopy(outputBytes, 0, combinedBytes, ivBytes.Length, outputBytes.Length);

                return Convert.ToBase64String(combinedBytes);
            }
        }

        public string Decrypt(string combinedData)
        {
            if (!IsBase64String(combinedData))
            {
                return combinedData;
            }


            // Convert the combined data (IV + ciphertext) back to bytes
            byte[] combinedBytes = Convert.FromBase64String(combinedData);

            // Extract the IV (first 16 bytes)
            byte[] ivBytes = new byte[16];
            Buffer.BlockCopy(combinedBytes, 0, ivBytes, 0, ivBytes.Length);

            // Extract the ciphertext (remaining bytes)
            byte[] ciphertext = new byte[combinedBytes.Length - ivBytes.Length];
            Buffer.BlockCopy(combinedBytes, ivBytes.Length, ciphertext, 0, ciphertext.Length);

            // Initialize the cipher for decryption
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
            cipher.Init(false, new ParametersWithIV(new KeyParameter(_encryptionKey), ivBytes));

            // Decrypt the ciphertext
            byte[] decryptedBytes = cipher.DoFinal(ciphertext);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
