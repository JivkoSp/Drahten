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
    public sealed class RemoveSearchedTopicDataHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<RemoveSearchedTopicDataCommand> _handler;

        private RemoveSearchedTopicDataCommand GetRemoveSearchedTopicDataCommand(Guid? topicId = null, Guid? userId = null,
            string searchedData = null, DateTimeOffset? dateTime = null)
        {
            var TopicId = topicId ?? Guid.NewGuid();
            var UserId = userId ?? Guid.NewGuid();
            var SearchedData = searchedData ?? "...";
            var DateTime = dateTime ?? DateTimeOffset.Now;

            var command = new RemoveSearchedTopicDataCommand(TopicId: TopicId, UserId: UserId,
                SearchedData: SearchedData, DateTime: DateTime);

            return command;
        }

        private SearchedTopicData GetSearchedTopicData()
        {
            var searchedTopicData = new SearchedTopicData(topicId: Guid.NewGuid(), userId: Guid.NewGuid(),
                searchedData: "...", dateTime: DateTimeOffset.Now);

            return searchedTopicData;
        }

        public RemoveSearchedTopicDataHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RemoveSearchedTopicDataHandler(_userRepository);
        }

        #endregion

        private Task Act(RemoveSearchedTopicDataCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeSearchedTopicDataCommand = GetRemoveSearchedTopicDataCommand();

            _userRepository.GetUserByIdAsync(removeSearchedTopicDataCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedTopicDataCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove SearchedTopicData value object if the UserId from the RemoveSearchedTopicDataCommand is valid Id for an existing User.
        //The SearchedTopicData value object to be removed must have the same values as those in the RemoveSearchedTopicDataCommand.
        //Ensure the repository is called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Removes_SearchedTopicData_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var searchedTopicData = GetSearchedTopicData();

            var removeSearchedTopicDataCommand = GetRemoveSearchedTopicDataCommand(
                searchedTopicData.TopicID, searchedTopicData.UserID, searchedTopicData.SearchedData, searchedTopicData.DateTime);

            user.AddSearchedTopicData(searchedTopicData);

            _userRepository.GetUserByIdAsync(removeSearchedTopicDataCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedTopicDataCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
