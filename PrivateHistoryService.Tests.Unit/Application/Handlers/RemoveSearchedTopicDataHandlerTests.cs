using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Application.Services.ReadServices;
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
        private readonly ISearchedTopicDataReadService _searchedTopicDataReadService;
        private readonly ICommandHandler<RemoveSearchedTopicDataCommand> _handler;

        private RemoveSearchedTopicDataCommand GetRemoveSearchedTopicDataCommand()
        {
            var command = new RemoveSearchedTopicDataCommand(UserId: Guid.NewGuid(), SearchedTopicDataId: Guid.NewGuid());

            return command;
        }

        private SearchedTopicData GetSearchedTopicData()
        {
            var searchedTopicData = new SearchedTopicData(topicId: Guid.NewGuid(), userId: Guid.NewGuid(),
                searchedData: "...", dateTime: DateTimeOffset.Now);

            return searchedTopicData;
        }

        private SearchedTopicDataDto GetSearchedTopicDataDto(SearchedTopicData searchedTopicData)
        {
            var searchedTopicDataDto = new SearchedTopicDataDto
            {
                SearchedTopicDataId = Guid.NewGuid(),
                TopicId = searchedTopicData.TopicID.Value,
                UserId = searchedTopicData.UserID.Value.ToString(),
                SearchedData = searchedTopicData.SearchedData,
                DateTime = searchedTopicData.DateTime
            };

            return searchedTopicDataDto;
        }

        public RemoveSearchedTopicDataHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _searchedTopicDataReadService = Substitute.For<ISearchedTopicDataReadService>();
            _handler = new RemoveSearchedTopicDataHandler(_userRepository, _searchedTopicDataReadService);
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

        //Should throw SearchedTopicNotFoundException when the following condition is met:
        //There is no SearchedTopicData returned from the repository that corresponds to the SearchedTopicDataId from the RemoveSearchedTopicDataCommand.
        [Fact]
        public async Task Throws_SearchedTopicNotFoundException_When_SearchedTopicData_With_Given_SearchedTopicDataId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var removeSearchedTopicDataCommand = GetRemoveSearchedTopicDataCommand();

            _userRepository.GetUserByIdAsync(removeSearchedTopicDataCommand.UserId).Returns(user);

            _searchedTopicDataReadService.GetSearchedTopicDataByIdAsync(removeSearchedTopicDataCommand.SearchedTopicDataId)
                .Returns(default(SearchedTopicDataDto));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedTopicDataCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SearchedTopicNotFoundException>();
        }

        //Should remove SearchedTopicData value object if the UserId and SearchedTopicDataId from the RemoveSearchedTopicDataCommand
        //are valid Ids for an existing User domain entity and SearchedTopicData value object.
        //The repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_And_SearchedTopicDataId_Removes_SearchedTopicData_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var searchedTopicData = GetSearchedTopicData();

            var searchedTopicDataDto = GetSearchedTopicDataDto(searchedTopicData);

            var removeSearchedTopicDataCommand = GetRemoveSearchedTopicDataCommand();

            user.AddSearchedTopicData(searchedTopicData);

            _userRepository.GetUserByIdAsync(removeSearchedTopicDataCommand.UserId).Returns(user);

            _searchedTopicDataReadService.GetSearchedTopicDataByIdAsync(removeSearchedTopicDataCommand.SearchedTopicDataId)
                .Returns(searchedTopicDataDto);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedTopicDataCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
