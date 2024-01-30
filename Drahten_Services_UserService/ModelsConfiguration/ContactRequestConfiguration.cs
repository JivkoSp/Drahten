using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ContactRequestConfiguration : IEntityTypeConfiguration<ContactRequest>
    {
        public void Configure(EntityTypeBuilder<ContactRequest> builder)
        {
            //Table name
            builder.ToTable("ContactRequest");

            //Primary key
            builder.HasKey(key => key.ContactRequestId);

            //Property config
            builder.Property(p => p.DateTime)
               .HasColumnType("timestamp without time zone")
               .IsRequired();

            //Relationships
            builder.HasOne(p => p.Sender)
                .WithMany()
                .HasForeignKey(p => p.SenderId)
                .HasConstraintName("FK_Sender_ContactRequest")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Receiver)
                .WithMany()
                .HasForeignKey(p => p.ReceiverId)
                .HasConstraintName("FK_Receiver_ContactRequest")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
