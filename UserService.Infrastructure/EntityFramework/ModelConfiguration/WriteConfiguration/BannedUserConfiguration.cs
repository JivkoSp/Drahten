using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class BannedUserConfiguration : IEntityTypeConfiguration<BannedUser>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public BannedUserConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<BannedUser> builder)
        {
            //Table name
            builder.ToTable("BannedUser");

            //Composite primary key
            builder.HasKey(key => new { key.IssuerUserId, key.ReceiverUserId });

            //Property config
            builder.Property(p => p.IssuerUserId)
                .HasConversion(new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x))))
                .HasColumnName("IssuerUserId")
                .IsRequired();

            builder.Property(p => p.ReceiverUserId)
               .HasConversion(new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x))))
               .HasColumnName("ReceiverUserId")
               .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
               .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
               .IsRequired();
        }
    }
}
