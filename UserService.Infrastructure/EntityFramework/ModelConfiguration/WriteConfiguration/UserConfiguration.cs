using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.Id);
            
            //Property config - Start

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("UserId");

            builder.Property(typeof(UserFullName), "_userFullName")
                .HasConversion(new EncryptedUserFullNameConverter(_encryptionProvider))
                .HasColumnName("UserFullName")
                .IsRequired();

            builder.Property(typeof(UserNickName), "_userNickName")
                .HasConversion(new EncryptedUserNickNameConverter(_encryptionProvider))
                .HasColumnName("UserNickName")
                .IsRequired();

            builder.Property(typeof(UserEmailAddress), "_userEmailAddress")
                .HasConversion(new EncryptedUserEmailAddressConverter(_encryptionProvider))
                .HasColumnName("UserEmailAddress")
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasMany(p => p.IssuedUserBans)
                .WithOne()
                .HasForeignKey("IssuerUserId");

            builder.HasMany(p => p.IssuedContactRequests)
                .WithOne()
                .HasForeignKey("IssuerUserId");

            builder.HasMany(p => p.ReceivedContactRequests)
                .WithOne()
                .HasForeignKey("ReceiverUserId");

            builder.HasMany(p => p.AuditTrail);
        }
    }
}
