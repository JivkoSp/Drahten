using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserTrackingConfiguration : IEntityTypeConfiguration<UserTracking>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserTrackingConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<UserTracking> builder)
        {
            //Table name
            builder.ToTable("UserTracking");

            //Shadow property for UserTrackingId.
            builder.Property<Guid>("UserTrackingId");

            //Primary key
            builder.HasKey("UserTrackingId");

            //Property config
            builder.Property(p => p.UserId)
              .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
              .HasColumnName("UserId");

            builder.Property(typeof(string), "Action")
               .HasConversion(new EncryptedStringConverter(_encryptionProvider))
               .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
               .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
               .IsRequired();

            builder.Property(typeof(string), "Referrer")
              .HasConversion(new EncryptedStringConverter(_encryptionProvider))
              .IsRequired();
        }
    }
}
