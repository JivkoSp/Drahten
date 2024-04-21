using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.ReadConfiguration
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<UserReadModel>
    {
        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            //Table name
            builder.ToTable("User");

            //Primary key
            builder.HasKey(key => key.UserId);

            builder.Property(p => p.UserId)
                .ValueGeneratedNever();
        }
    }
}
