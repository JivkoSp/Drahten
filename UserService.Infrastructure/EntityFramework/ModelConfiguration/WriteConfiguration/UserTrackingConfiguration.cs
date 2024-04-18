using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.ValueObjects;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserTrackingConfiguration : IEntityTypeConfiguration<UserTracking>
    {
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
               .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
               .IsRequired();

            builder.Property(typeof(string), "Referrer")
              .IsRequired();
        }
    }
}
