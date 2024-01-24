using Drahten_Services_UserService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Drahten_Services_UserService.ModelsConfiguration
{
    public class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLike>
    {
        public void Configure(EntityTypeBuilder<ArticleLike> builder)
        {
            //Table name
            builder.ToTable("ArticleLike");

            //Primary key
            builder.HasKey(p => p.ArticleLikeId);

            //Property config
            builder.Property(p => p.DateTime)
             .HasColumnType("timestamp without time zone")
             .ValueGeneratedOnAddOrUpdate()
             .IsRequired();

            //Relationships
            builder.HasOne(p => p.Article)
                .WithMany(p => p.ArticleLikes)
                .HasForeignKey(p => p.ArticleId)
                .HasConstraintName("FK_Article_ArticleLike")
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.User)
                .WithMany(p => p.ArticleLikes)
                .HasForeignKey(p => p.UserId)
                .HasConstraintName("FK_User_ArticleLike")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
