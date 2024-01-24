using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class PrivateHistoryConfiguration : IEntityTypeConfiguration<PrivateHistory>
    {
        public void Configure(EntityTypeBuilder<PrivateHistory> builder)
        {
            //Table name
            builder.ToTable("PrivateHistory");

            //Primary key
            builder.HasKey(key => key.UserId);

            //Property config - Start

            builder.Property(p => p.UserId)
                .ValueGeneratedNever();

            builder.Property(p => p.HistoryLiveTime)
              .HasColumnType("timestamp without time zone")
              .ValueGeneratedOnAddOrUpdate()
              .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<PrivateHistory>(p => p.UserId)
                .HasConstraintName("FK_User_PrivateHistory")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
