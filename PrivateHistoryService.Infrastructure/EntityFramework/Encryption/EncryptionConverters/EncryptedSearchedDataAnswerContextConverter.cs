using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PrivateHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters
{
    internal sealed class EncryptedSearchedDataAnswerContextConverter : ValueConverter<SearchedDataAnswerContext, string>
    {
        public EncryptedSearchedDataAnswerContextConverter(IEncryptionProvider encryptionProvider, ConverterMappingHints mappingHints = null)
            : base(
                  v => ConvertToString(v, encryptionProvider),
                  v => ConvertToSearchedDataAnswerContext(v, encryptionProvider),
                  mappingHints)
        {
        }

        private static string ConvertToString(string searchedDataAnswerContext, IEncryptionProvider encryptionProvider)
        {
            // Encrypt the string
            string encryptedValue = encryptionProvider.Encrypt(searchedDataAnswerContext);

            return encryptedValue;
        }

        private static SearchedDataAnswerContext ConvertToSearchedDataAnswerContext(string value, IEncryptionProvider encryptionProvider)
        {
            // Decrypt the string
            string decryptedValue = encryptionProvider.Decrypt(value);

            // Convert string to SearchedDataAnswer.
            return new SearchedDataAnswerContext(decryptedValue);
        }
    }
}
