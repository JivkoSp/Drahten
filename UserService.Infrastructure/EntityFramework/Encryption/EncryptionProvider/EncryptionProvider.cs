using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System.Security.Cryptography;
using System.Text;

namespace UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider
{
    // Provides encryption and decryption services using AES encryption.
    public sealed class EncryptionProvider : IEncryptionProvider
    {
        // Stores the encryption key as a byte array.
        private readonly byte[] _encryptionKey;

        public EncryptionProvider(string encryptionKey)
        {
            _encryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
        }

        /// <summary>
        /// Checks if a string is a valid Base64 string.
        /// </summary>
        /// <param name="base64">The string to check.</param>
        /// <returns>True if the string is valid Base64 and its length is a multiple of 16; otherwise, false.</returns>
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

        /// <summary>
        /// Encrypts the specified plaintext using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext to encrypt.</param>
        /// <returns>The encrypted text in Base64 format.</returns>
        public string Encrypt(string plaintext)
        {
            // Generate a random Initialization Vector (IV)
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.GenerateIV();
                byte[] ivBytes = aesAlg.IV;

                // Initialize the cipher for encryption using AES with CBC mode and PKCS7 padding
                IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
                cipher.Init(true, new ParametersWithIV(new KeyParameter(_encryptionKey), ivBytes));

                var inputBytes = Encoding.UTF8.GetBytes(plaintext);

                // Encrypt the plaintext
                var outputBytes = cipher.DoFinal(inputBytes);

                // Combine the IV and the ciphertext(prepend IV to ciphertext)
                byte[] combinedBytes = new byte[ivBytes.Length + outputBytes.Length];
                Buffer.BlockCopy(ivBytes, 0, combinedBytes, 0, ivBytes.Length);
                Buffer.BlockCopy(outputBytes, 0, combinedBytes, ivBytes.Length, outputBytes.Length);

                // Convert the combined IV and ciphertext to a Base64 string
                return Convert.ToBase64String(combinedBytes);
            }
        }

        /// <summary>
        /// Decrypts the specified encrypted text using AES decryption.
        /// </summary>
        /// <param name="combinedData">The encrypted text in Base64 format.</param>
        /// <returns>The decrypted plaintext.</returns>
        public string Decrypt(string combinedData)
        {
            // Check if the input is a valid Base64 string
            if (!IsBase64String(combinedData))
            {
                return combinedData;
            }

            // Convert the combined data (IV + ciphertext) back to a byte array
            byte[] combinedBytes = Convert.FromBase64String(combinedData);

            // Extract the IV (first 16 bytes)
            byte[] ivBytes = new byte[16];
            Buffer.BlockCopy(combinedBytes, 0, ivBytes, 0, ivBytes.Length);

            // Extract the ciphertext (remaining bytes after the IV)
            byte[] ciphertext = new byte[combinedBytes.Length - ivBytes.Length];
            Buffer.BlockCopy(combinedBytes, ivBytes.Length, ciphertext, 0, ciphertext.Length);

            // Initialize the cipher for decryption using AES with CBC mode and PKCS7 padding
            IBufferedCipher cipher = CipherUtilities.GetCipher("AES/CBC/PKCS7Padding");
            cipher.Init(false, new ParametersWithIV(new KeyParameter(_encryptionKey), ivBytes));

            // Decrypt the ciphertext
            byte[] decryptedBytes = cipher.DoFinal(ciphertext);

            // Convert the decrypted byte array back to a string
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
