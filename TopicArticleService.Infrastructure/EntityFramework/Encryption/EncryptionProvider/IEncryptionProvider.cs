
namespace TopicArticleService.Infrastructure.EntityFramework.Encryption.EncryptionProvider
{
    public interface IEncryptionProvider
    {
        string Encrypt(string plaintext);
        string Decrypt(string combinedData);
    }
}
