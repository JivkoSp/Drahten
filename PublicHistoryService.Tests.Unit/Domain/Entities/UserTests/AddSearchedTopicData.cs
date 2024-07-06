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
    public sealed class AddSearchedTopicData
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

        public AddSearchedTopicData()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw SearchedTopicDataAlreadyExistsException when the following condition is met:
        //Another SearchedTopicData value object with the same values already exists.
        [Fact]
        public void Duplicate_SearchedTopicData_Throws_SearchedTopicDataAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var searchedTopicData = GetSearchedTopicData();

            user.AddSearchedTopicData(searchedTopicData);

            //ACT
            var exception = Record.Exception(() => user.AddSearchedTopicData(searchedTopicData));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SearchedTopicDataAlreadyExistsException>();
        }

        //Should add SearchedTopicData value object to internal collection of SearchedTopicData value objects
        //and produce SearchedTopicDataAdded domain event.
        //The SearchedTopicDataAdded domain event should contain:
        //1. The same user entity to which the SearchedTopicData value object was added.
        //2. The same SearchedTopicData value object that was added to the internal collection of SearchedTopicData value objects.
        [Fact]
        public void Adds_SearchedTopicData_And_Produces_SearchedTopicDataAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var searchedTopicData = GetSearchedTopicData();

            //ACT
            var exception = Record.Exception(() => user.AddSearchedTopicData(searchedTopicData));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.SearchedTopicInformation.Count().ShouldBe(1);

            var searchedTopicDataAddedEvent = user.DomainEvents.FirstOrDefault() as SearchedTopicDataAdded;

            searchedTopicDataAddedEvent.ShouldNotBeNull();

            searchedTopicDataAddedEvent.User.ShouldBeSameAs(user);

            searchedTopicDataAddedEvent.SearchedTopicData.ShouldBeSameAs(searchedTopicData);
        }
    }
}
