using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.ValueObjects;

namespace TopicArticleService.Domain.Entities
{
    public class Article : AggregateRoot<ArticleID>
    {
        private ArticlePrevTitle _prevTitle;
        private ArticleTitle _title;
        private ArticleContent _content;
        private ArticlePublishingDate _publishingDate;
        private ArticleAuthor _author;
        private ArticleLink _link;
        private TopicId _topicId;
        private HashSet<UserArticle> _userArticles = new HashSet<UserArticle>();
        private List<ArticleComment> _articleComments = new List<ArticleComment>();
        //Reason to use HasHet<T> for ArticleLike and ArticleDislike:
        //One of the domain requirements is to have unique user - article like and user - article dislike.
        //That means that one user can have one like or one dislike for one article.
        //In addition to that he HashSet offers constant-time performance for basic operations such as adding, removing,
        //and checking for the existence of an element (Add, Remove, Contains). 
        private HashSet<ArticleLike> _articleLikes = new HashSet<ArticleLike>();
        private HashSet<ArticleDislike> _articleDislikes = new HashSet<ArticleDislike>();

        public IReadOnlyCollection<UserArticle> UserArticles
        {
            get { return new ReadOnlyCollection<UserArticle>(_userArticles.ToList()); }
        }

        public IReadOnlyCollection<ArticleComment> ArticleComments
        {
            get { return new ReadOnlyCollection<ArticleComment>(_articleComments); }
        }
        
        public IReadOnlyCollection<ArticleLike> ArticleLikes
        {
            get { return new ReadOnlyCollection<ArticleLike>(_articleLikes.ToList()); }
        }

        public IReadOnlyCollection<ArticleDislike> ArticleDislikes
        {
            get { return new ReadOnlyCollection<ArticleDislike>(_articleDislikes.ToList()); }
        }

        private Article()
        {
        }

        internal Article(ArticleID id, ArticlePrevTitle prevTitle, ArticleTitle title, ArticleContent content,
            ArticlePublishingDate publishingDate, ArticleAuthor author, ArticleLink link, TopicId topicId)
        {
            Id = id;
            _prevTitle = prevTitle;
            _title = title;
            _content = content;
            _publishingDate = publishingDate;
            _author = author;
            _link = link;
            _topicId = topicId;
        }

        public void AddUserArticle(UserArticle userArticle)
        {
            var alreadyExists = _userArticles.Contains(userArticle);

            if (alreadyExists)
            {
                throw new UserArticleAlreadyExistsException(userArticle.UserID, userArticle.ArticleId);
            }

            _userArticles.Add(userArticle);

            //TODO: Add event
        }

        public void AddComment(ArticleComment comment)
        {
            var alreadyExists = _articleComments.Any(x => x.HasUserId(comment) == true 
                    && x.GetParentCommentId() == comment.GetParentCommentId());

            if (alreadyExists)
            {
                throw new ArticleCommentAlreadyExistsException(Id, comment.GetUserId());
            }

            _articleComments.Add(comment);

            AddEvent(new ArticleCommentAdded(this, comment));
        }

        public void RemoveComment(ArticleCommentID articleCommentId, ArticleCommentID parentArticleCommentId)
        {
            if(parentArticleCommentId != null)
            {
                var parentComment = _articleComments.FirstOrDefault(x => x.Id == parentArticleCommentId);

                if (parentComment == null)
                {
                    throw new ArticleCommentParentNotFoundException(parentArticleCommentId, articleCommentId);
                }

                //parentComment.RemoveChildComment(articleCommentId);
            }

            var articleComment = _articleComments.FirstOrDefault(x => x.Id == articleCommentId);

            if(articleComment == null)
            {
                throw new ArticleCommentNotFoundException(articleCommentId);
            }

            _articleComments.Remove(articleComment);

            AddEvent(new ArticleCommentRemoved(this, articleComment));
        }

        public void AddLike(ArticleLike like)
        {
            var alreadyExists = _articleLikes.Contains(like);

            if(alreadyExists)
            {
                throw new ArticleLikeAlreadyExistsException(Id, like.UserID);
            }

            //Find the user dislike in _dislikes.
            var userDislike = _articleDislikes.FirstOrDefault(x => x.UserID == like.UserID);

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
                throw new ArticleDislikeAlreadyExistsException(Id, dislike.UserID);
            }

            //Find the user like in _likes.
            var userLike = _articleLikes.FirstOrDefault(x => x.UserID == dislike.UserID);

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
