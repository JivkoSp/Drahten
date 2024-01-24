using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ViewedUserPublicHistConfiguration : IEntityTypeConfiguration<ViewedUserPublicHist>
    {
        public void Configure(EntityTypeBuilder<ViewedUserPublicHist> builder)
        {
            //Table name
            builder.ToTable("ViewedUserPublicHist");

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
                .HasForeignKey<ViewedUserPublicHist>(p => p.ViewedUserId)
                .HasConstraintName("FK_User_ViewedUserPublicHist")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.PublicHistory)
                .WithMany(p => p.ViewedUsers)
                .HasForeignKey(p => p.PublicHistoryId)
                .HasConstraintName("FK_PublicHistory_ViewedUserPublicHist")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
