using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.Id);

            //Property config
            builder.Property(p => p.Id)
               .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
               .HasColumnName("UserId")
               .ValueGeneratedNever()
               .IsRequired();
        }
    }
}
