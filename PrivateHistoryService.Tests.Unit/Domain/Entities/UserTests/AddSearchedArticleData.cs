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
    public sealed class AddSearchedArticleData
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
                searchedData: "...", searchedDataAnswer: ".....", searchedDataAnswerContext: "......", dateTime: DateTimeOffset.Now);

            return searchedArticleData;
        }

        public AddSearchedArticleData()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw SearchedArticleDataAlreadyExistsException when the following condition is met:
        //Another SearchedArticleData value object with the same values already exists.
        [Fact]
        public void Duplicate_SearchedArticleData_Throws_SearchedArticleDataAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var searchedArticleData = GetSearchedArticleData();

            user.AddSearchedArticleData(searchedArticleData);

            //ACT
            var exception = Record.Exception(() => user.AddSearchedArticleData(searchedArticleData));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SearchedArticleDataAlreadyExistsException>();
        }

        //Should add SearchedArticleData value object to internal collection of SearchedArticleData value objects
        //and produce SearchedArticleDataAdded domain event.
        //The SearchedArticleDataAdded domain event should contain:
        //1. The same user entity to which the SearchedArticleData value object was added.
        //2. The same SearchedArticleData value object that was added to the internal collection of SearchedArticleData value objects.
        [Fact]
        public void Adds_SearchedArticleData_And_Produces_SearchedArticleDataAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var searchedArticleData = GetSearchedArticleData();

            //ACT
            var exception = Record.Exception(() => user.AddSearchedArticleData(searchedArticleData));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.SearchedArticleInformation.Count().ShouldBe(1);

            var searchedArticleDataAddedEvent = user.DomainEvents.FirstOrDefault() as SearchedArticleDataAdded;

            searchedArticleDataAddedEvent.ShouldNotBeNull();

            searchedArticleDataAddedEvent.User.ShouldBeSameAs(user);

            searchedArticleDataAddedEvent.SearchedArticleData.ShouldBeSameAs(searchedArticleData);
        }
    }
}
