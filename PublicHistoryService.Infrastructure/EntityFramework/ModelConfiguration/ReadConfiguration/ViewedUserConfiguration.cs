using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionConverters;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class ViewedUserConfiguration : IEntityTypeConfiguration<ViewedUserReadModel>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public ViewedUserConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

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
                .HasConversion(new EncryptedDateTimeOffsetConverter(_encryptionProvider))
                .IsRequired();

            //Property config - End

            //Relationships
            builder.HasOne(p => p.ViewedUser)
                .WithMany(p => p.ViewedUsers)
                .HasForeignKey(p => p.ViewerUserId)
                .HasConstraintName("FK_User_ViewedUsers")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
