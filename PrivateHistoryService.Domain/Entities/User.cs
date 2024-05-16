using PrivateHistoryService.Domain.Events;
using PrivateHistoryService.Domain.Exceptions;
using PrivateHistoryService.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace PrivateHistoryService.Domain.Entities
{
    public class User : AggregateRoot<UserID>
    {
        private HashSet<ViewedArticle> _viewedArticles;
        private List<SubscribedTopic> _subscribedTopics;
        private List<SearchedArticleData> _searchedArticleInformation;
        private List<SearchedTopicData> _searchedTopicInformation;
        private List<CommentedArticle> _commentedArticles;
        private List<LikedArticle> _likedArticles;
        private List<DislikedArticle> _dislikedArticles;
        private List<LikedArticleComment> _likedArticleComments;
        private List<DislikedArticleComment> _dislikedArticleComments;
        private List<ViewedUser> _viewedUsers;

        public IReadOnlyCollection<ViewedArticle> ViewedArticles
        {
            get { return new ReadOnlyCollection<ViewedArticle>(_viewedArticles.ToList()); }
        }

        public IReadOnlyCollection<SubscribedTopic> SubscribedTopics
        {
            get { return new ReadOnlyCollection<SubscribedTopic>(_subscribedTopics); }
        }

        public IReadOnlyCollection<SearchedArticleData> SearchedArticleInformation
        {
            get { return new ReadOnlyCollection<SearchedArticleData>(_searchedArticleInformation); }
        }

        public IReadOnlyCollection<SearchedTopicData> SearchedTopicInformation
        {
            get { return new ReadOnlyCollection<SearchedTopicData>(_searchedTopicInformation); }
        }

        public IReadOnlyCollection<CommentedArticle> CommentedArticles
        {
            get { return new ReadOnlyCollection<CommentedArticle>(_commentedArticles); }
        }

        public IReadOnlyCollection<LikedArticle> LikedArticles
        {
            get { return new ReadOnlyCollection<LikedArticle>(_likedArticles); }
        }

        public IReadOnlyCollection<DislikedArticle> DislikedArticles
        {
            get { return new ReadOnlyCollection<DislikedArticle>(_dislikedArticles); }
        }

        public IReadOnlyCollection<LikedArticleComment> LikedArticleComments
        {
            get { return new ReadOnlyCollection<LikedArticleComment>(_likedArticleComments); }
        }

        public IReadOnlyCollection<DislikedArticleComment> DislikedArticleComments
        {
            get { return new ReadOnlyCollection<DislikedArticleComment>(_dislikedArticleComments); }
        }

        public IReadOnlyCollection<ViewedUser> ViewedUsers
        {
            get { return new ReadOnlyCollection<ViewedUser>(_viewedUsers); }
        }

        private User()
        {
        }

        internal User(UserID userId)
        {
            ValidateConstructorParameters<NullUserParametersException>([userId]);

            Id = userId;

            _viewedArticles = new HashSet<ViewedArticle>();
            _subscribedTopics = new List<SubscribedTopic>();
            _searchedArticleInformation = new List<SearchedArticleData>();
            _searchedTopicInformation = new List<SearchedTopicData>();
            _commentedArticles = new List<CommentedArticle>();
            _likedArticles = new List<LikedArticle>();  
            _dislikedArticles = new List<DislikedArticle>();
            _likedArticleComments = new List<LikedArticleComment>();
            _dislikedArticleComments = new List<DislikedArticleComment>();
            _viewedUsers = new List<ViewedUser>();
        }

        public void AddViewedArticle(ViewedArticle viewedArticle)
        {
            var alreadyExists = _viewedArticles.Contains(viewedArticle);

            if (alreadyExists)
            {
                throw new ViewedArticleAlreadyExistsException(viewedArticle.ArticleID, viewedArticle.UserID, viewedArticle.DateTime);
            }

            _viewedArticles.Add(viewedArticle);

            AddEvent(new ViewedArticleAdded(this, viewedArticle));
        }
    }
}
