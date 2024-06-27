using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequest>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ContactRequestConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            //Table name
            builder.ToTable("ContactRequest");

            //Composite primary key
            builder.HasKey(key => new { key.IssuerUserId, key.ReceiverUserId });

            var userIdConverter = new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x)));

            //Property config
            builder.Property(p => p.IssuerUserId)
                .HasConversion(userIdConverter)
                .HasColumnName("IssuerUserId")
                .IsRequired();

            builder.Property(p => p.ReceiverUserId)
               .HasConversion(userIdConverter)
               .HasColumnName("ReceiverUserId")
               .IsRequired();

            builder.Property(typeof(string), "Message")
               .HasConversion(new EncryptedStringConverter(_encryptionProvider));

            builder.Property(typeof(DateTimeOffset), "DateTime")
               .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
               .IsRequired();
        }
    }
}
