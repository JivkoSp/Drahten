using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ViewedUserConfiguration : IEntityTypeConfiguration<ViewedUser>
    {
        public void Configure(EntityTypeBuilder<ViewedUser> builder)
        {
            //Table name
            builder.ToTable("ViewedUser");

            //Shadow property for ViewedUserReadModelId.
            builder.Property<Guid>("ViewedUserReadModelId");

            //Primary key
            builder.HasKey("ViewedUserReadModelId");

            //Property config - Start

            builder.Property(p => p.ViewerUserID)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("ViewerUserId")
                .IsRequired();

            builder.Property(p => p.ViewedUserID)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("ViewedUserId")
                .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
             .IsRequired();

            //Property config - End
        }
    }
}
