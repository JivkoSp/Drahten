using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Events;
using PublicHistoryService.Domain.Exceptions;
using PublicHistoryService.Domain.Factories;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PublicHistoryService.Tests.Unit.Domain.Entities.UserTests
{
    public sealed class RemoveViewedArticle
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

        public RemoveViewedArticle()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw ViewedArticleNotFoundException when the following condition is met:
        //There is no ViewedArticle value object with the same values.
        [Fact]
        public void ViewedArticle_NotFound_Throws_ViewedArticleNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var viewedArticle = GetViewedArticle();

            //ACT
            var exception = Record.Exception(() => user.RemoveViewedArticle(viewedArticle));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ViewedArticleNotFoundException>();
        }

        //Should remove ViewedArticle value object from internal collection of ViewedArticle value objects
        //and produce ViewedArticleRemoved domain event.
        //The ViewedArticleRemoved domain event should contain:
        //1. The same user entity that the ViewedArticle value object was removed from.
        //2. The same ViewedArticle value object that was removed from the internal collection of ViewedArticle value objects.
        [Fact]
        public void Removes_ViewedArticle_And_Produces_ViewedArticleRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var viewedArticle = GetViewedArticle();

            user.AddViewedArticle(viewedArticle);

            //ACT
            var exception = Record.Exception(() => user.RemoveViewedArticle(viewedArticle));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.ViewedArticles.Count().ShouldBe(0);

            var viewedArticleAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ViewedArticleAdded)) as ViewedArticleAdded;

            var viewedArticleRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ViewedArticleRemoved)) as ViewedArticleRemoved;

            viewedArticleAddedEvent.ShouldNotBeNull();

            viewedArticleAddedEvent.User.ShouldBeSameAs(user);

            viewedArticleAddedEvent.ViewedArticle.ShouldBeSameAs(viewedArticle);

            viewedArticleRemovedEvent.ShouldNotBeNull();

            viewedArticleRemovedEvent.User.ShouldBeSameAs(user);

            viewedArticleRemovedEvent.ViewedArticle.ShouldBeSameAs(viewedArticle);
        }
    }
}
