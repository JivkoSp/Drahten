using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class ArticleComment : AggregateRoot<ArticleCommentId>
    {
        private ArticleCommentValue _commentValue;
        private ArticleCommentDateTime _dateTime;
        private UserId _userId;
        private List<ArticleComment> _articleComments = new List<ArticleComment>();
        private HashSet<ArticleCommentLike> _articleCommentLikes = new HashSet<ArticleCommentLike>();
        private HashSet<ArticleCommentDislike> _articleCommentDislikes = new HashSet<ArticleCommentDislike>();

        public ArticleComment(ArticleCommentId id, ArticleCommentValue commentValue, ArticleCommentDateTime dateTime, UserId userId)
        {
            Id = id;
            _commentValue = commentValue;
            _dateTime = dateTime;
            _userId = userId;
        }

        public void AddChildComment(ArticleComment comment)
        {
            var alreadyExists = _articleComments.Any(x => x._userId == comment._userId);

            if (alreadyExists)
            {
                throw new ArticleCommentChildAlreadyExistsException(Id, comment._userId);
            }

            _articleComments.Add(comment);

            AddEvent(new ArticleCommentChildAdded(this, comment));
        }

        public void RemoveChildComment(ArticleCommentId id)
        {
            var childComment = _articleComments.FirstOrDefault(x => x.Id == id);

            if(childComment == null)
            {
                throw new ArticleCommentNotFoundException(id, _userId);
            }

            _articleComments.Remove(childComment);

            AddEvent(new ArticleCommentChildRemoved(this, childComment));
        }

        public void AddLike(ArticleCommentLike articleCommentLike)
        {
            var alreadyExists = _articleCommentLikes.Contains(articleCommentLike);

            if(alreadyExists)
            {
                throw new ArticleCommentLikeAlreadyExistsException(Id, articleCommentLike.UserId);
            }

            //Search for user dislike from _articleCommentDislikes for this comment.
            var userDislike = _articleCommentDislikes.FirstOrDefault(x => x.UserId == articleCommentLike.UserId);

            //Check if the user for this like has dislike in _articleCommentDislikes.
            if(userDislike != null)
            {
                //The user for this like has dislike in _articleCommentDislikes.

                //Delete the dislike from _articleCommentDislikes.
                _articleCommentDislikes.Remove(userDislike);
            }

            _articleCommentLikes.Add(articleCommentLike);

            AddEvent(new ArticleCommentLikeAdded(this, articleCommentLike));
        }

        public void AddDislike(ArticleCommentDislike articleCommentDislike)
        {
            var alreadyExists = _articleCommentDislikes.Contains(articleCommentDislike);

            if( alreadyExists)
            {
                throw new ArticleCommentDisLikeAlreadyExistsException(Id, articleCommentDislike.UserId);
            }

            //Search for user like from _articleCommentLikes for this comment.
            var userLike = _articleCommentLikes.FirstOrDefault(x => x.UserId == articleCommentDislike.UserId);

            //Check if the user for this dislike has like in _articleCommentLikes.
            if(userLike != null)
            {
                //The user for this dislike has like in _articleCommentLikes.

                //Delete the like from _articleCommentLikes.
                _articleCommentLikes.Remove(userLike);
            }

            _articleCommentDislikes.Add(articleCommentDislike);

            AddEvent(new ArticleCommentDislikeAdded(this, articleCommentDislike));  
        }

        public bool HasUserId(ArticleComment articleComment)
            => _userId == articleComment._userId;

        public UserId GetUserId()
            => _userId;
    }
}
