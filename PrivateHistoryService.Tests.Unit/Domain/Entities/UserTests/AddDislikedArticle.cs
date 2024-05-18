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
    public sealed class AddDislikedArticle
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private DislikedArticle GetDislikedArticle()
        {
            var dislikedArticle = new DislikedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return dislikedArticle;
        }

        public AddDislikedArticle()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw DislikedArticleAlreadyExistsException when the following condition is met:
        //Another DislikedArticle value object with the same ArticleID and UserID already exists.
        [Fact]
        public void Duplicate_DislikedArticle_Throws_DislikedArticleAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var dislikedArticle = GetDislikedArticle();

            user.AddDislikedArticle(dislikedArticle);

            //ACT
            var exception = Record.Exception(() => user.AddDislikedArticle(dislikedArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<DislikedArticleAlreadyExistsException>();
        }

        //Should add DislikedArticle value object to internal collection of DislikedArticle value objects
        //and produce DislikedArticleAdded domain event.
        //The DislikedArticleAdded domain event should contain:
        //1. The same user entity to which the DislikedArticle value object was added.
        //2. The same DislikedArticle value object that was added to the internal collection of DislikedArticle value objects.
        [Fact]
        public void Adds_DislikedArticle_And_Produces_DislikedArticleAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var dislikedArticle = GetDislikedArticle();

            //ACT
            var exception = Record.Exception(() => user.AddDislikedArticle(dislikedArticle));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.DislikedArticles.Count().ShouldBe(1);

            var dislikedArticleAddedEvent = user.DomainEvents.FirstOrDefault() as DislikedArticleAdded;

            dislikedArticleAddedEvent.ShouldNotBeNull();

            dislikedArticleAddedEvent.User.ShouldBeSameAs(user);

            dislikedArticleAddedEvent.DislikedArticle.ShouldBeSameAs(dislikedArticle);
        }
    }
}
