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
    public sealed class RemoveSearchedTopicData
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private SearchedTopicData GetSearchedTopicData()
        {
            var searchedData = new SearchedData("...");

            var searchedTopicData = new SearchedTopicData(topicId: Guid.NewGuid(), userId: Guid.NewGuid(),
                searchedData: searchedData, dateTime: DateTimeOffset.Now);

            return searchedTopicData;
        }

        public RemoveSearchedTopicData()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw SearchedTopicDataNotFoundException when the following condition is met:
        //There is no SearchedTopicData value object with the same values.
        [Fact]
        public void SearchedTopicData_NotFound_Throws_SearchedTopicDataNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var searchedTopicData = GetSearchedTopicData();

            //ACT
            var exception = Record.Exception(() => user.RemoveSearchedTopicData(searchedTopicData));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SearchedTopicDataNotFoundException>();
        }

        //Should remove SearchedTopicData value object from internal collection of SearchedTopicData value objects
        //and produce SearchedTopicDataRemoved domain event.
        //The SearchedTopicDataRemoved domain event should contain:
        //1. The same user entity that the SearchedTopicData value object was removed from.
        //2. The same SearchedTopicData value object that was removed from the internal collection of SearchedTopicData value objects.
        [Fact]
        public void Removes_SearchedTopicData_And_Produces_SearchedTopicDataRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var searchedTopicData = GetSearchedTopicData();

            user.AddSearchedTopicData(searchedTopicData);

            //ACT
            var exception = Record.Exception(() => user.RemoveSearchedTopicData(searchedTopicData));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.SearchedTopicInformation.Count().ShouldBe(0);

            var searchedTopicDataAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(SearchedTopicDataAdded)) as SearchedTopicDataAdded;

            var searchedTopicDataRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(SearchedTopicDataRemoved)) as SearchedTopicDataRemoved;

            searchedTopicDataAddedEvent.ShouldNotBeNull();

            searchedTopicDataAddedEvent.User.ShouldBeSameAs(user);

            searchedTopicDataAddedEvent.SearchedTopicData.ShouldBeSameAs(searchedTopicData);

            searchedTopicDataRemovedEvent.ShouldNotBeNull();

            searchedTopicDataRemovedEvent.User.ShouldBeSameAs(user);

            searchedTopicDataRemovedEvent.SearchedTopicData.ShouldBeSameAs(searchedTopicData);
        }
    }
}
