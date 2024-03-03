using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.UserId);

            builder.Property(p => p.UserId)
                .ValueGeneratedNever();

            builder.Property(p => p.FullName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.NickName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.EmailAddress)
                .IsRequired();
        }
    }
}
