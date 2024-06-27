using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserTrackingConfiguration : IEntityTypeConfiguration<UserTrackingReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserTrackingConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<UserTrackingReadModel> builder)
        {
            //Table name
            builder.ToTable("UserTracking");

            //Primary key
            builder.HasKey(key => key.UserTrackingId);

            //Property config
            builder.Property(p => p.Action)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(p => p.DateTime)
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            builder.Property(p => p.Referrer)
                .HasConversion(new EncryptedStringConverter(_encryptionProvider))
                .IsRequired();

            //Relationships
            builder.HasOne(p => p.User)
               .WithMany(p => p.AuditTrail)
               .HasForeignKey(p => p.UserId)
               .HasConstraintName("FK_User_AuditTrail")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
