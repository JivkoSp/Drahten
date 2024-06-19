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
    public sealed class RemovedSearchedArticleData
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private SearchedArticleData GetSearchedArticleData()
        {
            var searchedArticleData = new SearchedArticleData(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                searchedData: "...", searchedDataAnswer: ".....", searchedDataAnswerContext: ".......", dateTime: DateTimeOffset.Now);

            return searchedArticleData;
        }

        public RemovedSearchedArticleData()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw SearchedArticleDataNotFoundException when the following condition is met:
        //There is no SearchedArticleData value object with the same values.
        [Fact]
        public void SearchedArticleData_NotFound_Throws_SearchedArticleDataNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var searchedArticleData = GetSearchedArticleData();

            //ACT
            var exception = Record.Exception(() => user.RemoveSearchedArticleData(searchedArticleData));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SearchedArticleDataNotFoundException>();
        }

        //Should remove SearchedArticleData value object from internal collection of SearchedArticleData value objects
        //and produce SearchedArticleDataRemoved domain event.
        //The SearchedArticleDataRemoved domain event should contain:
        //1. The same user entity that the SearchedArticleData value object was removed from.
        //2. The same SearchedArticleData value object that was removed from the internal collection of SearchedArticleData value objects.
        [Fact]
        public void Removes_SearchedArticleData_And_Produces_SearchedArticleDataRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var searchedArticleData = GetSearchedArticleData();

            user.AddSearchedArticleData(searchedArticleData);

            //ACT
            var exception = Record.Exception(() => user.RemoveSearchedArticleData(searchedArticleData));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.SearchedArticleInformation.Count().ShouldBe(0);

            var searchedArticleDataAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(SearchedArticleDataAdded)) as SearchedArticleDataAdded;

            var searchedArticleDataRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(SearchedArticleDataRemoved)) as SearchedArticleDataRemoved;

            searchedArticleDataAddedEvent.ShouldNotBeNull();

            searchedArticleDataAddedEvent.User.ShouldBeSameAs(user);

            searchedArticleDataAddedEvent.SearchedArticleData.ShouldBeSameAs(searchedArticleData);

            searchedArticleDataRemovedEvent.ShouldNotBeNull();

            searchedArticleDataRemovedEvent.User.ShouldBeSameAs(user);

            searchedArticleDataRemovedEvent.SearchedArticleData.ShouldBeSameAs(searchedArticleData);
        }
    }
}
