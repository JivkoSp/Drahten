using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Encryption.EncryptionProvider;

namespace PublicHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private readonly IEncryptionProvider _encryptionProvider;

        public UserConfiguration(IEncryptionProvider encryptionProvider)
        {
            _encryptionProvider = encryptionProvider;
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.Id);

            //Property config - Start

            builder.Property(p => p.Id)
               .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
               .HasColumnName("UserId")
               .ValueGeneratedNever()
               .IsRequired();

            //Property config - End

            //Relationships
            builder.HasMany(p => p.ViewedArticles);

            builder.HasMany(p => p.ViewedUsers)
               .WithOne()
               .HasForeignKey("ViewerUserId");

            builder.HasMany(p => p.SearchedArticleInformation);

            builder.HasMany(p => p.SearchedTopicInformation);

            builder.HasMany(p => p.CommentedArticles);
        }
    }
}
