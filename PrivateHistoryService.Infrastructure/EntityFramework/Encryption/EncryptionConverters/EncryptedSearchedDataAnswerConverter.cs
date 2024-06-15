using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedSearchedDataAnswerConverter : ValueConverter<SearchedDataAnswer, string>
    {
        public EncryptedSearchedDataAnswerConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToSearchedDataAnswer(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToString(string searchedData, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(searchedData);

            return encryptedValue;
        }

        private static SearchedDataAnswer ConvertToSearchedDataAnswer(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to SearchedDataAnswer.
            return new SearchedDataAnswer(decryptedValue);
        }
    }
}
