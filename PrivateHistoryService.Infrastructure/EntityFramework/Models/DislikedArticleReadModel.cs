﻿
namespace PrivateHistoryService.Infrastructure.EntityFramework.Models
{
    internal class DislikedArticleReadModel
    {
        //Composite primary key { ArticleId, UserId }
        public string ArticleId { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset DateTime { get; set; }

        //Relationships
        public virtual UserReadModel User { get; set; }
    }
}
