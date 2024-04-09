using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class ArticleCommentLikeConfiguration : IEntityTypeConfiguration<ArticleCommentLike>
    {
        public void Configure(EntityTypeBuilder<ArticleCommentLike> builder)
        {
            //Table name
            builder.ToTable("ArticleCommentLike");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleCommentID, key.UserID });

            var userIdConverter = new ValueConverter<UserID, string>(x => x.Value.ToString(), x => new UserID(Guid.Parse(x)));

            var articleCommentIdConverter = new ValueConverter<ArticleCommentID, Guid>(x => x.Value, x => new ArticleCommentID(x));

            //Property config - Start

            builder.Property(p => p.ArticleCommentID)
                .HasConversion(articleCommentIdConverter)
                .HasColumnName("ArticleCommentId")
                .IsRequired();

            builder.Property(p => p.UserID)
                .HasConversion(userIdConverter)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(p => p.DateTime)
                .IsRequired();

            //Property config - End
        }
    }
}
