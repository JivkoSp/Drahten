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

            builder.Property(p => p.ViewerUserId)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .IsRequired();

            builder.Property(p => p.ViewedUserId)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
             .IsRequired();

            //Property config - End
        }
    }
}
