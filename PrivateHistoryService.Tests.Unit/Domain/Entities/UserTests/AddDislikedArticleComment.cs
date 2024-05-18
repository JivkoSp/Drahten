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
    public sealed class AddDislikedArticleComment
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private DislikedArticleComment GetDislikedArticleComment()
        {
            var dislikedArticleComment = new DislikedArticleComment(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                articleCommentId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return dislikedArticleComment;
        }

        public AddDislikedArticleComment()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw DislikedArticleCommentAlreadyExistsException when the following condition is met:
        //Another DislikedArticleComment value object with the same ArticleID, UserID and ArticleCommentID already exists.
        [Fact]
        public void Duplicate_DislikedArticleComment_Throws_DislikedArticleCommentAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var dislikedArticleComment = GetDislikedArticleComment();

            user.AddDislikedArticleComment(dislikedArticleComment);

            //ACT
            var exception = Record.Exception(() => user.AddDislikedArticleComment(dislikedArticleComment));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<DislikedArticleCommentAlreadyExistsException>();
        }

        //Should add DislikedArticleComment value object to internal collection of DislikedArticleComment value objects
        //and produce DislikedArticleCommentAdded domain event.
        //The DislikedArticleCommentAdded domain event should contain:
        //1. The same user entity to which the DislikedArticleComment value object was added.
        //2. The same DislikedArticleComment value object that was added to the internal collection of DislikedArticleComment value objects.
        [Fact]
        public void Adds_DislikedArticleComment_And_Produces_DislikedArticleCommentAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var dislikedArticleComment = GetDislikedArticleComment();

            //ACT
            var exception = Record.Exception(() => user.AddDislikedArticleComment(dislikedArticleComment));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.DislikedArticleComments.Count().ShouldBe(1);

            var dislikedArticleCommentAddedEvent = user.DomainEvents.FirstOrDefault() as DislikedArticleCommentAdded;

            dislikedArticleCommentAddedEvent.ShouldNotBeNull();

            dislikedArticleCommentAddedEvent.User.ShouldBeSameAs(user);

            dislikedArticleCommentAddedEvent.DislikedArticleComment.ShouldBeSameAs(dislikedArticleComment);
        }
    }
}
