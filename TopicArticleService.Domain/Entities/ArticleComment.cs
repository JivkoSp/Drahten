using System.Collections.ObjectModel;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class ArticleComment : AggregateRoot<ArticleCommentID>
    {
        private ArticleCommentValue _commentValue;
        private ArticleCommentDateTime _dateTime;
        private UserID _userId;
        private ArticleCommentID _parentArticleCommentId;
        private HashSet<ArticleCommentLike> _articleCommentLikes = new HashSet<ArticleCommentLike>();
        private HashSet<ArticleCommentDislike> _articleCommentDislikes = new HashSet<ArticleCommentDislike>();

        public IReadOnlyCollection<ArticleCommentLike> ArticleCommentLikes
        {
            get { return new ReadOnlyCollection<ArticleCommentLike>(_articleCommentLikes.ToList()); }
        }

        public IReadOnlyCollection<ArticleCommentDislike> ArticleCommentDislikes
        {
            get { return new ReadOnlyCollection<ArticleCommentDislike>(_articleCommentDislikes.ToList()); }
        }

        private ArticleComment()
        {
        }
        
        internal ArticleComment(ArticleCommentID id, ArticleCommentValue commentValue, ArticleCommentDateTime dateTime, UserID userId,
                ArticleCommentID parentId)
        {
            Id = id;
            _commentValue = commentValue;
            _dateTime = dateTime;
            _userId = userId;
            _parentArticleCommentId = parentId;
        }

        public void AddLike(ArticleCommentLike articleCommentLike)
        {
            var alreadyExists = _articleCommentLikes.Contains(articleCommentLike);

            if(alreadyExists)
            {
                throw new ArticleCommentLikeAlreadyExistsException(Id, articleCommentLike.UserID);
            }

            //Search for user dislike from _articleCommentDislikes for this comment.
            var userDislike = _articleCommentDislikes.FirstOrDefault(x => x.UserID == articleCommentLike.UserID);

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
                throw new ArticleCommentDisLikeAlreadyExistsException(Id, articleCommentDislike.UserID);
            }

            //Search for user like from _articleCommentLikes for this comment.
            var userLike = _articleCommentLikes.FirstOrDefault(x => x.UserID == articleCommentDislike.UserID);

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

        internal bool HasUserId(ArticleComment articleComment)
            => _userId == articleComment._userId;

        internal UserID GetUserId()
            => _userId;

        internal ArticleCommentID GetParentCommentId()
            => _parentArticleCommentId;
    }
}
