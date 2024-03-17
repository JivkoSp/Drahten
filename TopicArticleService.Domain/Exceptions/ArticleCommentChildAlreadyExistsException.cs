using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicArticleService.Domain.Exceptions
{
    public class ArticleCommentChildAlreadyExistsException : DomainException
    {
        public ArticleCommentChildAlreadyExistsException(Guid articleCommentId, string userId) 
            : base(message: $"ArticleComment #{articleCommentId} already has comment from user #{userId}!")
        {
        }
    }
}
