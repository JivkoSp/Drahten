using PublicHistoryService.Domain.Events;
using PublicHistoryService.Domain.Exceptions;
using PublicHistoryService.Domain.ValueObjects;
using System.Collections.ObjectModel;

namespace PublicHistoryService.Domain.Entities
{
    public class User : AggregateRoot<UserID>
    {
        private HashSet<ViewedArticle> _viewedArticles = new HashSet<ViewedArticle>();
        private HashSet<ViewedUser> _viewedUsers = new HashSet<ViewedUser>();
        private HashSet<SearchedArticleData> _searchedArticleInformation = new HashSet<SearchedArticleData>();
        private HashSet<SearchedTopicData> _searchedTopicInformation = new HashSet<SearchedTopicData>();
        private HashSet<CommentedArticle> _commentedArticles = new HashSet<CommentedArticle>();

        public IReadOnlyCollection<ViewedArticle> ViewedArticles
        {
            get { return new ReadOnlyCollection<ViewedArticle>(_viewedArticles.ToList()); }
        }

        public IReadOnlyCollection<ViewedUser> ViewedUsers
        {
            get { return new ReadOnlyCollection<ViewedUser>(_viewedUsers.ToList()); }
        }

        public IReadOnlyCollection<SearchedArticleData> SearchedArticleInformation
        {
            get { return new ReadOnlyCollection<SearchedArticleData>(_searchedArticleInformation.ToList()); }
        }

        public IReadOnlyCollection<SearchedTopicData> SearchedTopicInformation
        {
            get { return new ReadOnlyCollection<SearchedTopicData>(_searchedTopicInformation.ToList()); }
        }

        public IReadOnlyCollection<CommentedArticle> CommentedArticles
        {
            get { return new ReadOnlyCollection<CommentedArticle>(_commentedArticles.ToList()); }
        }

        private User()
        {
        }

        internal User(UserID userId)
        {
            ValidateConstructorParameters<NullUserParametersException>([userId]);

            Id = userId;
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

        public void RemoveViewedArticle(ViewedArticle viewedArticle)
        {
            var alreadyExists = _viewedArticles.Contains(viewedArticle);

            if (alreadyExists == false)
            {
                throw new ViewedArticleNotFoundException(viewedArticle.ArticleID, viewedArticle.UserID, viewedArticle.DateTime);
            }

            _viewedArticles.Remove(viewedArticle);

            AddEvent(new ViewedArticleRemoved(this, viewedArticle));
        }

        public void AddViewedUser(ViewedUser viewedUser)
        {
            var alreadyExists = _viewedUsers.Contains(viewedUser);

            if (alreadyExists)
            {
                throw new ViewedUserAlreadyExistsException(viewedUser.ViewedUserId, viewedUser.ViewerUserId);
            }

            _viewedUsers.Add(viewedUser);

            AddEvent(new ViewedUserAdded(this, viewedUser));
        }

        public void RemoveViewedUser(ViewedUser viewedUser)
        {
            var alreadyExists = _viewedUsers.Contains(viewedUser);

            if (alreadyExists == false)
            {
                throw new ViewedUserNotFoundException(viewedUser.ViewedUserId, viewedUser.ViewerUserId, viewedUser.DateTime);
            }

            _viewedUsers.Remove(viewedUser);

            AddEvent(new ViewedUserRemoved(this, viewedUser));
        }

        public void AddSearchedArticleData(SearchedArticleData searchedArticleData)
        {
            var alreadyExists = _searchedArticleInformation.Contains(searchedArticleData);

            if (alreadyExists)
            {
                throw new SearchedArticleDataAlreadyExistsException(searchedArticleData.UserID, searchedArticleData.DateTime);
            }

            _searchedArticleInformation.Add(searchedArticleData);

            AddEvent(new SearchedArticleDataAdded(this, searchedArticleData));
        }

        public void RemoveSearchedArticleData(SearchedArticleData searchedArticleData)
        {
            var alreadyExists = _searchedArticleInformation.Contains(searchedArticleData);

            if (alreadyExists == false)
            {
                throw new SearchedArticleDataNotFoundException(searchedArticleData.ArticleID, searchedArticleData.UserID, searchedArticleData.DateTime);
            }

            _searchedArticleInformation.Remove(searchedArticleData);

            AddEvent(new SearchedArticleDataRemoved(this, searchedArticleData));
        }

        public void AddSearchedTopicData(SearchedTopicData searchedTopicData)
        {
            var alreadyExists = _searchedTopicInformation.Contains(searchedTopicData);

            if (alreadyExists)
            {
                throw new SearchedTopicDataAlreadyExistsException(searchedTopicData.UserID, searchedTopicData.DateTime);
            }

            _searchedTopicInformation.Add(searchedTopicData);

            AddEvent(new SearchedTopicDataAdded(this, searchedTopicData));
        }

        public void RemoveSearchedTopicData(SearchedTopicData searchedTopicData)
        {
            var alreadyExists = _searchedTopicInformation.Contains(searchedTopicData);

            if (alreadyExists == false)
            {
                throw new SearchedTopicDataNotFoundException(searchedTopicData.TopicID, searchedTopicData.UserID, searchedTopicData.DateTime);
            }

            _searchedTopicInformation.Remove(searchedTopicData);

            AddEvent(new SearchedTopicDataRemoved(this, searchedTopicData));
        }

        public void AddCommentedArticle(CommentedArticle commentedArticle)
        {
            var alreadyExists = _commentedArticles.Contains(commentedArticle);

            if (alreadyExists)
            {
                throw new CommentedArticleAlreadyExistsException(commentedArticle.UserID, commentedArticle.DateTime);
            }

            _commentedArticles.Add(commentedArticle);

            AddEvent(new CommentedArticleAdded(this, commentedArticle));
        }

        public void RemoveCommentedArticle(CommentedArticle commentedArticle)
        {
            var alreadyExists = _commentedArticles.Contains(commentedArticle);

            if (alreadyExists == false)
            {
                throw new CommentedArticleNotFoundException(commentedArticle.ArticleID, commentedArticle.UserID,
                    commentedArticle.ArticleComment, commentedArticle.DateTime);
            }

            _commentedArticles.Remove(commentedArticle);

            AddEvent(new CommentedArticleRemoved(this, commentedArticle));
        }
    }
}
