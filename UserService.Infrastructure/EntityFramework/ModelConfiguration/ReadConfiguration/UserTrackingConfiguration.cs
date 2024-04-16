
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserTrackingConfiguration : IEntityTypeConfiguration<UserTrackingReadModel>
    {
        public void Configure(EntityTypeBuilder<UserTrackingReadModel> builder)
        {
            //Table name
            builder.ToTable("UserTracking");

            //Primary key
            builder.HasKey(key => key.UserTrackingId);

            //Property config
            builder.Property(p => p.Action)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .IsRequired();

            builder.Property(p => p.Referrer)
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
