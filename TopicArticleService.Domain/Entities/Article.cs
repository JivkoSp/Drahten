using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class Article : AggregateRoot<ArticleId>
    {
        private ArticlePrevTitle _prevTitle;
        private ArticleTitle _title;
        private ArticleContent _content;
        private ArticlePublishingDate _publishingDate;
        private ArticleAuthor _author;
        private ArticleLink _link;
        private Topic _topic;
        private List<ArticleComment> _articleComments = new List<ArticleComment>();
        //Reason to use HasHet<T> for ArticleLike and ArticleDislike:
        //One of the domain requirements is to have unique user - article like and user - article dislike.
        //That means that one user can have one like or one dislike for one article.
        //In addition to that he HashSet offers constant-time performance for basic operations such as adding, removing,
        //and checking for the existence of an element (Add, Remove, Contains). 
        private HashSet<ArticleLike> _articleLikes = new HashSet<ArticleLike>();
        private HashSet<ArticleDislike> _articleDislikes = new HashSet<ArticleDislike>();

        internal Article(ArticleId id, ArticlePrevTitle prevTitle, ArticleTitle title, ArticleContent content,
            ArticlePublishingDate publishingDate, ArticleAuthor author, ArticleLink link, Topic topic)
        {
            Id = id;
            _prevTitle = prevTitle;
            _title = title;
            _content = content;
            _publishingDate = publishingDate;
            _author = author;
            _link = link;
            _topic = topic;
        }

        public void AddComment(ArticleComment comment)
        {
            var alreadyExists = _articleComments.Any(x => x.HasUserId(comment) == true);

            if(alreadyExists)
            {
               throw new ArticleCommentAlreadyExistsException(Id, comment.GetUserId());
            }

            _articleComments.Add(comment);

            AddEvent(new ArticleCommentAdded(this, comment));
        }

        public void RemoveComment(ArticleComment comment)
        {
            var articleComment = _articleComments.FirstOrDefault(x => x.GetUserId() == comment.GetUserId());

            if (articleComment == null)
            {
                throw new ArticleCommentNotFoundException(comment.Id, comment.GetUserId());
            }

            _articleComments.Remove(comment);

            AddEvent(new ArticleCommentRemoved(this, comment));
        }

        public void AddLike(ArticleLike like)
        {
            var alreadyExists = _articleLikes.Contains(like);

            if(alreadyExists)
            {
                throw new ArticleLikeAlreadyExistsException(Id, like.UserId);
            }

            //Find the user dislike in _dislikes.
            var userDislike = _articleDislikes.FirstOrDefault(x => x.UserId == like.UserId);

            //Check if the user for this like has dislike in _dislikes.
            if(userDislike != null)
            {
                //The user for this like has dislike in _dislikes.

                //Delete the dislike from _dislikes.
                _articleDislikes.Remove(userDislike);
            }

            _articleLikes.Add(like);

            AddEvent(new ArticleLikeAdded(this, like));
        }

        public void AddDislike(ArticleDislike dislike)
        {
            var alreadyExists = _articleDislikes.Contains(dislike);

            if(alreadyExists)
            {
                throw new ArticleDislikeAlreadyExistsException(Id, dislike.UserId);
            }

            //Find the user like in _likes.
            var userLike = _articleLikes.FirstOrDefault(x => x.UserId == dislike.UserId);

            //Check if the user for this dislike has like in _likes.
            if(userLike != null)
            {
                //The user for this dislike has like in _likes.

                //Delete the like from _likes.
                _articleLikes.Remove(userLike);
            }

            _articleDislikes.Add(dislike);

            AddEvent(new ArticleDislikeAdded(this, dislike));
        }
    }
}
