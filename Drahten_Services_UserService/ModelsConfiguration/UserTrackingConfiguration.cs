using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class UserTrackingConfiguration : IEntityTypeConfiguration<UserTracking>
    {
        public void Configure(EntityTypeBuilder<UserTracking> builder)
        {
            //Table name
            builder.ToTable("UserTracking");

            //Primary key
            builder.HasKey(key => key.UserTrackingId);

            //Relationships
            builder.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<UserTracking>(p => p.UserId)
                .HasConstraintName("FK_User_UserTracking")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
