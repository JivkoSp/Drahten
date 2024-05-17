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
    public sealed class AddViewedArticle
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private ViewedArticle GetViewedArticle()
        {
            var viewedArticle = new ViewedArticle(articleId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return viewedArticle;
        }

        public AddViewedArticle()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw ViewedArticleAlreadyExistsException when the following condition is met:
        //Another ViewedArticle value object with the same values already exists.
        [Fact]
        public void Duplicate_ViewedArticle_Throws_ViewedArticleAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var viewedArticle = GetViewedArticle();

            user.AddViewedArticle(viewedArticle);

            //ACT
            var exception = Record.Exception(() => user.AddViewedArticle(viewedArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ViewedArticleAlreadyExistsException>();
        }

        //Should add ViewedArticle value object to internal collection of ViewedArticle value objects
        //and produce ViewedArticleAdded domain event.
        //The ViewedArticleAdded domain event should contain:
        //1. The same user entity to which the ViewedArticle value object was added.
        //2. The same ViewedArticle value object that was added to the internal collection of ViewedArticle value objects.
        [Fact]
        public void Adds_ViewedArticle_And_Produces_ViewedArticleAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var viewedArticle = GetViewedArticle();

            //ACT
            var exception = Record.Exception(() => user.AddViewedArticle(viewedArticle));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.ViewedArticles.Count().ShouldBe(1);

            var viewedArticleAddedEvent = user.DomainEvents.FirstOrDefault() as ViewedArticleAdded;

            viewedArticleAddedEvent.ShouldNotBeNull();

            viewedArticleAddedEvent.User.ShouldBeSameAs(user);

            viewedArticleAddedEvent.ViewedArticle.ShouldBeSameAs(viewedArticle);
        }
    }
}
