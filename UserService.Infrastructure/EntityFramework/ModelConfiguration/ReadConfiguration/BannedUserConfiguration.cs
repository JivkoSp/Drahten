﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using UserService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class BannedUserConfiguration : IEntityTypeConfiguration<BannedUserReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public BannedUserConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<BannedUserReadModel> builder)
        {
            //Table name
            builder.ToTable("BannedUser");

            //Composite primary key
            builder.HasKey(key => new { key.IssuerUserId, key.ReceiverUserId });

            //Property config
            builder.Property(p => p.DateTime)
             .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.Issuer)
                .WithMany(p => p.IssuedBansByUser)
                .HasForeignKey(p => p.IssuerUserId)
                .HasConstraintName("FK_Issuer_BannedUsers")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Receiver)
                .WithMany(p => p.ReceivedBansForUser)
                .HasForeignKey(p => p.ReceiverUserId)
                .HasConstraintName("FK_Receiver_BannedUsers")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
