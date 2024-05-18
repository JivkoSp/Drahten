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
    public sealed class RemoveCommentedArticle
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

        public RemoveCommentedArticle()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw CommentedArticleNotFoundException when the following condition is met:
        //There is no CommentedArticle value object with the same values.
        [Fact]
        public void CommentedArticle_NotFound_Throws_CommentedArticleNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var commentedArticle = GetCommentedArticle();

            //ACT
            var exception = Record.Exception(() => user.RemoveCommentedArticle(commentedArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<CommentedArticleNotFoundException>();
        }

        //Should remove CommentedArticle value object from internal collection of CommentedArticle value objects
        //and produce CommentedArticleRemoved domain event.
        //The CommentedArticleRemoved domain event should contain:
        //1. The same user entity that the CommentedArticle value object was removed from.
        //2. The same CommentedArticle value object that was removed from the internal collection of CommentedArticle value objects.
        [Fact]
        public void Removes_CommentedArticle_And_Produces_CommentedArticleRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var commentedArticle = GetCommentedArticle();

            user.AddCommentedArticle(commentedArticle);

            //ACT
            var exception = Record.Exception(() => user.RemoveCommentedArticle(commentedArticle));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.CommentedArticles.Count().ShouldBe(0);

            var commentedArticleAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(CommentedArticleAdded)) as CommentedArticleAdded;

            var commentedArticleRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(CommentedArticleRemoved)) as CommentedArticleRemoved;

            commentedArticleAddedEvent.ShouldNotBeNull();

            commentedArticleAddedEvent.User.ShouldBeSameAs(user);

            commentedArticleAddedEvent.CommentedArticle.ShouldBeSameAs(commentedArticle);

            commentedArticleRemovedEvent.ShouldNotBeNull();

            commentedArticleRemovedEvent.User.ShouldBeSameAs(user);

            commentedArticleRemovedEvent.CommentedArticle.ShouldBeSameAs(commentedArticle);
        }
    }
}
