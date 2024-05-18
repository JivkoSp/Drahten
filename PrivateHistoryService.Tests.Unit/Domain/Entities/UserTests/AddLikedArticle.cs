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
    public sealed class AddLikedArticle
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private LikedArticle GetLikedArticle()
        {
            var likedArticle = new LikedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return likedArticle;
        }

        public AddLikedArticle()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw LikedArticleAlreadyExistsException when the following condition is met:
        //Another LikedArticle value object with the same ArticleID and UserID already exists.
        [Fact]
        public void Duplicate_LikedArticle_Throws_LikedArticleAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var likedArticle = GetLikedArticle();

            user.AddLikedArticle(likedArticle);

            //ACT
            var exception = Record.Exception(() => user.AddLikedArticle(likedArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<LikedArticleAlreadyExistsException>();
        }

        //Should add LikedArticle value object to internal collection of LikedArticle value objects
        //and produce LikedArticleAdded domain event.
        //The LikedArticleAdded domain event should contain:
        //1. The same user entity to which the LikedArticle value object was added.
        //2. The same LikedArticle value object that was added to the internal collection of LikedArticle value objects.
        [Fact]
        public void Adds_LikedArticle_And_Produces_LikedArticleAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var likedArticle = GetLikedArticle();

            //ACT
            var exception = Record.Exception(() => user.AddLikedArticle(likedArticle));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.LikedArticles.Count().ShouldBe(1);

            var likedArticleAddedEvent = user.DomainEvents.FirstOrDefault() as LikedArticleAdded;

            likedArticleAddedEvent.ShouldNotBeNull();

            likedArticleAddedEvent.User.ShouldBeSameAs(user);

            likedArticleAddedEvent.LikedArticle.ShouldBeSameAs(likedArticle);
        }
    }
}
