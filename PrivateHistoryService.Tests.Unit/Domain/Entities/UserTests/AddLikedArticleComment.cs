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
    public sealed class AddLikedArticleComment
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private LikedArticleComment GetLikedArticleComment()
        {
            var likedArticleComment = new LikedArticleComment(articleId: Guid.NewGuid(), userId: Guid.NewGuid(), 
                articleCommentId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return likedArticleComment;
        }

        public AddLikedArticleComment()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw LikedArticleCommentAlreadyExistsException when the following condition is met:
        //Another LikedArticleComment value object with the same ArticleID, UserID and ArticleCommentID already exists.
        [Fact]
        public void Duplicate_LikedArticleComment_Throws_LikedArticleCommentAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var likedArticleComment = GetLikedArticleComment();

            user.AddLikedArticleComment(likedArticleComment);

            //ACT
            var exception = Record.Exception(() => user.AddLikedArticleComment(likedArticleComment));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<LikedArticleCommentAlreadyExistsException>();
        }

        //Should add LikedArticleComment value object to internal collection of LikedArticleComment value objects
        //and produce LikedArticleCommentAdded domain event.
        //The LikedArticleCommentAdded domain event should contain:
        //1. The same user entity to which the LikedArticleComment value object was added.
        //2. The same LikedArticleComment value object that was added to the internal collection of LikedArticleComment value objects.
        [Fact]
        public void Adds_LikedArticleComment_And_Produces_LikedArticleCommentAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var likedArticleComment = GetLikedArticleComment();

            //ACT
            var exception = Record.Exception(() => user.AddLikedArticleComment(likedArticleComment));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.LikedArticleComments.Count().ShouldBe(1);

            var likedArticleCommentAddedEvent = user.DomainEvents.FirstOrDefault() as LikedArticleCommentAdded;

            likedArticleCommentAddedEvent.ShouldNotBeNull();

            likedArticleCommentAddedEvent.User.ShouldBeSameAs(user);

            likedArticleCommentAddedEvent.LikedArticleComment.ShouldBeSameAs(likedArticleComment);
        }
    }
}
