
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Events;
using PrivateHistoryService.Domain.Exceptions;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Domain.Entities.UserTests
{
    public sealed class AddCommentedArticle
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private CommentedArticle GetCommentedArticle()
        {
            var articleComment = new ArticleComment("...");

            var commentedArticle = new CommentedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                articleComment: articleComment, dateTime: DateTimeOffset.Now);

            return commentedArticle;
        }

        public AddCommentedArticle()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw CommentedArticleAlreadyExistsException when the following condition is met:
        //Another CommentedArticle value object with the same values already exists.
        [Fact]
        public void Duplicate_CommentedArticle_Throws_CommentedArticleAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var commentedArticle = GetCommentedArticle();

            user.AddCommentedArticle(commentedArticle);

            //ACT
            var exception = Record.Exception(() => user.AddCommentedArticle(commentedArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<CommentedArticleAlreadyExistsException>();
        }

        //Should add CommentedArticle value object to internal collection of CommentedArticle value objects
        //and produce CommentedArticleAdded domain event.
        //The CommentedArticleAdded domain event should contain:
        //1. The same user entity to which the CommentedArticle value object was added.
        //2. The same CommentedArticle value object that was added to the internal collection of CommentedArticle value objects.
        [Fact]
        public void Adds_CommentedArticle_And_Produces_CommentedArticleAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var commentedArticle = GetCommentedArticle();

            //ACT
            var exception = Record.Exception(() => user.AddCommentedArticle(commentedArticle));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.CommentedArticles.Count().ShouldBe(1);

            var commentedArticleAddedEvent = user.DomainEvents.FirstOrDefault() as CommentedArticleAdded;

            commentedArticleAddedEvent.ShouldNotBeNull();

            commentedArticleAddedEvent.User.ShouldBeSameAs(user);

            commentedArticleAddedEvent.CommentedArticle.ShouldBeSameAs(commentedArticle);
        }
    }
}
