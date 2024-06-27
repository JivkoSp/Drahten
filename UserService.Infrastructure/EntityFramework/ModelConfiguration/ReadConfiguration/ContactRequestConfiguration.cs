using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequestReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ContactRequestConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<ContactRequestReadModel> builder)
        {
            //Table name
            builder.ToTable("ContactRequest");

            //Composite primary key
            builder.HasKey(key => new { key.IssuerUserId, key.ReceiverUserId });

            //Property config
            builder.Property(p => p.Message)
             .HasConversion(new EncryptedStringConverter(_encryptionProvider));

            builder.Property(p => p.DateTime)
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.Issuer)
                .WithMany(p => p.IssuedContactRequests)
                .HasForeignKey(p => p.IssuerUserId)
                .HasConstraintName("FK_Issuer_ContactRequests")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Receiver)
               .WithMany(p => p.ReceivedContactRequests)
               .HasForeignKey(p => p.ReceiverUserId)
               .HasConstraintName("FK_Receiver_ContactRequests")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
