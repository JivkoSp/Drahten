using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
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

            //Property config 
            builder.Property(p => p.UserFullName)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(p => p.UserNickName)
                .IsRequired();

            builder.Property(p => p.UserEmailAddress)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();
        }
    }
}
