using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Domain.Entities;
using UserService.Domain.ValueObjects;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.Id);

            var fullNameConverter = new ValueConverter<UserFullName, string>(x => x, x => new UserFullName(x));

            var nickNameConverter = new ValueConverter<UserNickName, string>(x => x, x => new UserNickName(x));

            var emailAddressConverter = new ValueConverter<UserEmailAddress, string>(x => x, x => new UserEmailAddress(x));

            //Property config - Start

            builder.Property(p => p.Id)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("UserId");

            builder.Property(typeof(UserFullName), "_userFullName")
                .HasConversion(fullNameConverter)
                .HasColumnName("UserFullName")
                .IsRequired();

            builder.Property(typeof(UserNickName), "_userNickName")
                .HasConversion(nickNameConverter)
                .HasColumnName("UserNickName")
                .IsRequired();

            builder.Property(typeof(UserEmailAddress), "_userEmailAddress")
                .HasConversion(emailAddressConverter)
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
