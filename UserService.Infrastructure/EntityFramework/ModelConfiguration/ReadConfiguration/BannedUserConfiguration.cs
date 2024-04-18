﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class BannedUserConfiguration : IEntityTypeConfiguration<BannedUserReadModel>
    {
        public void Configure(EntityTypeBuilder<BannedUserReadModel> builder)
        {
            //Table name
            builder.ToTable("BannedUser");

            //Composite primary key
            builder.HasKey(key => new { key.IssuerUserId, key.ReceiverUserId });

            //Relationships
            builder.HasOne(p => p.Issuer)
                .WithMany(p => p.BannedUsers)
                .HasForeignKey(p => p.IssuerUserId)
                .HasConstraintName("FK_Issuer_BannedUsers")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Receiver)
                .WithMany(p => p.BannedUsers)
                .HasForeignKey(p => p.ReceiverUserId)
                .HasConstraintName("FK_Receiver_BannedUsers")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}