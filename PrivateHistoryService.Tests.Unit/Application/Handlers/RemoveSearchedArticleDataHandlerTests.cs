using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
using PrivateHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class RemoveSearchedArticleDataHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<RemoveSearchedArticleDataCommand> _handler;

        private RemoveSearchedArticleDataCommand GetRemoveSearchedArticleDataCommand(Guid? articleId = null, Guid? userId = null,
            string searchedData = null, DateTimeOffset? dateTime = null)
        {
            var ArticleId = articleId ?? Guid.NewGuid();
            var UserId = userId ?? Guid.NewGuid();
            var SearchedData = searchedData ?? "...";
            var DateTime = dateTime ?? DateTimeOffset.Now;

            var command = new RemoveSearchedArticleDataCommand(ArticleId: ArticleId, UserId: UserId,
                SearchedData: SearchedData, DateTime: DateTime);

            return command;
        }

        private SearchedArticleData GetSearchedArticleData()
        {
            var searchedArticleData = new SearchedArticleData(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                searchedData: "...", dateTime: DateTimeOffset.Now);

            return searchedArticleData;
        }

        public RemoveSearchedArticleDataHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RemoveSearchedArticleDataHandler(_userRepository);
        }

        #endregion

        private Task Act(RemoveSearchedArticleDataCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeSearchedArticleDataCommand = GetRemoveSearchedArticleDataCommand();

            _userRepository.GetUserByIdAsync(removeSearchedArticleDataCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedArticleDataCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove SearchedArticleData value object if the UserId from the RemoveSearchedArticleDataCommand is valid Id for an existing User.
        //The SearchedArticleData value object to be removed must have the same values as those in the RemoveSearchedArticleDataCommand.
        //Ensure the repository is called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Removes_SearchedArticleData_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var searchedArticleData = GetSearchedArticleData();

            var removeSearchedArticleDataCommand = GetRemoveSearchedArticleDataCommand(
                searchedArticleData.ArticleID, searchedArticleData.UserID, searchedArticleData.SearchedData, searchedArticleData.DateTime);

            user.AddSearchedArticleData(searchedArticleData);

            _userRepository.GetUserByIdAsync(removeSearchedArticleDataCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedArticleDataCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
