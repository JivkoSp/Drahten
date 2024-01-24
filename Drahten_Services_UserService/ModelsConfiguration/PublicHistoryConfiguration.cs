using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class PublicHistoryConfiguration : IEntityTypeConfiguration<PublicHistory>
    {
        public void Configure(EntityTypeBuilder<PublicHistory> builder)
        {
            //Table name
            builder.ToTable("PublicHistory");

            //Primary key
            builder.HasKey(key => key.UserId);

            //Property config 
            builder.Property(p => p.UserId)
                .ValueGeneratedNever();

            //Relationships
            builder.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<PublicHistory>(p => p.UserId)
                .HasConstraintName("FK_User_PublicHistory")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
