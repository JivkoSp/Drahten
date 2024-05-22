﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrivateHistoryService.Domain.ValueObjects;

namespace PrivateHistoryService.Infrastructure.EntityFramework.ModelConfiguration.WriteConfiguration
{
    internal sealed class LikedArticleConfiguration : IEntityTypeConfiguration<LikedArticle>
    {
        public void Configure(EntityTypeBuilder<LikedArticle> builder)
        {
            //Table name
            builder.ToTable("LikedArticle");

            //Composite primary key
            builder.HasKey(key => new { key.ArticleID, key.UserID });

            //Property config - Start

            #region ConversionToUUID
            // *** IMPORTANT! ***
            // Convert Guid to string without hyphens.
            // Explanation: UUIDs are commonly represented as a 32-character hexadecimal string (such as "37955c6317e6869e90fe0029564078c8")
            // or as a 36-character string with hyphens separating groups of characters (such as "37955c63-17e6-869e-90fe-0029564078c8").
            // The Id is NOT stored as Guid in the database, because PostgreSQL will automatically format it with hyphens for readability
            // and standardization. This will cause problems when accessing documents with their Id's from the SearchService database store
            // (a Elasticserach instance). The Elasticsearch instance will expect the Id's to be in a 32-character hexadecimal format and 
            // will not be able to find the document's if it receaves a 36-character hexadecimal format for the Id.
            // ---------------------------------------
            // !!! The "N" in the ToString method is used to format the Guid without hyphens (or dashes).
            #endregion
            builder.Property(p => p.ArticleID)
                .HasConversion(id => id.Value.ToString("N"), id => new ArticleID(Guid.ParseExact(id, "N")))
                .HasColumnName("ArticleId")
                .IsRequired();

            builder.Property(p => p.UserID)
                .HasConversion(id => id.Value.ToString(), id => new UserID(Guid.Parse(id)))
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(typeof(DateTimeOffset), "DateTime")
              .IsRequired();

            //Property config - End
        }
    }
}