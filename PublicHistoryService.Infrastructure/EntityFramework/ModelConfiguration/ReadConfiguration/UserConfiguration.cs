using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<UserReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.UserId);

            builder.Property(p => p.UserId)
                .ValueGeneratedNever();
        }
    }
}
