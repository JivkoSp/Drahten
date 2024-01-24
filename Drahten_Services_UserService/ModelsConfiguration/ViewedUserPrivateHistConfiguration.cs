using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ViewedUserPrivateHistConfiguration : IEntityTypeConfiguration<ViewedUserPrivateHist>
    {
        public void Configure(EntityTypeBuilder<ViewedUserPrivateHist> builder)
        {
            //Table name
            builder.ToTable("ViewedUserPrivateHist");

            //Primary key
            builder.HasKey(key => key.ViewedUserId);

            //Property config - Start

            builder.Property(p => p.ViewedUserId)
                .ValueGeneratedNever();

            builder.Property(p => p.DateTime)
             .HasColumnType("timestamp without time zone")
             .ValueGeneratedOnAddOrUpdate()
             .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<ViewedUserPrivateHist>(p => p.ViewedUserId)
                .HasConstraintName("FK_User_ViewedUserPrivateHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PrivateHistory)
                .WithMany(p => p.ViewedUsers)
                .HasForeignKey(p => p.PrivateHistoryId)
                .HasConstraintName("FK_PrivateHistory_ViewedUserPrivateHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
