using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequestReadModel>
    {
        public void Configure(EntityTypeBuilder<ContactRequestReadModel> builder)
        {
            //Table name
            builder.ToTable("ContactRequest");

            //Composite primary key
            builder.HasKey(key => new { key.IssuerUserId, key.ReceiverUserId });

            //Property config
            builder.Property(p => p.DateTime)
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.Issuer)
                .WithMany(p => p.ContactRequests)
                .HasForeignKey(p => p.IssuerUserId)
                .HasConstraintName("FK_Issuer_ContactRequests")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Receiver)
               .WithMany(p => p.ContactRequests)
               .HasForeignKey(p => p.ReceiverUserId)
               .HasConstraintName("FK_Receiver_ContactRequests")
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
