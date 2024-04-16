using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<UserReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.UserId);

            //Property config 
            builder.Property(p => p.UserFullName)
                .IsRequired();

            builder.Property(p => p.UserNickName)
                .IsRequired();

            builder.Property(p => p.UserEmailAddress)
                .IsRequired();
        }
    }
}
