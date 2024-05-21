using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ViewedUserConfiguration : IEntityTypeConfiguration<ViewedUserReadModel>
    {
        public void Configure(EntityTypeBuilder<ViewedUserReadModel> builder)
        {
            //Table name
            builder.ToTable("ViewedUser");

            //Primary key
            builder.HasKey(key => key.ViewedUserReadModelId);

            //Property config - Start

            builder.Property(p => p.ViewerUserId)
                .IsRequired();

            builder.Property(p => p.ViewedUserId)
                .IsRequired();

            builder.Property(p => p.DateTime)
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.ViewedUser)
                .WithMany(p => p.ViewedUsers)
                .HasForeignKey(p => p.ViewedUserId)
                .HasConstraintName("FK_User_ViewedUsers")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
