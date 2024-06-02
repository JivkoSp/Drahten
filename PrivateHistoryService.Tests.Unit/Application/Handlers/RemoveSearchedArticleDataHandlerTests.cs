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
    public sealed class RemoveSearchedArticleDataHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ISearchedArticleDataReadService _searchedArticleDataReadService;
        private readonly ICommandHandler<RemoveSearchedArticleDataCommand> _handler;

        private RemoveSearchedArticleDataCommand GetRemoveSearchedArticleDataCommand()
        {
            var command = new RemoveSearchedArticleDataCommand(UserId: Guid.NewGuid(), SearchedArticleDataId: Guid.NewGuid());

            return command;
        }

        private SearchedArticleData GetSearchedArticleData()
        {
            var searchedArticleData = new SearchedArticleData(articleId: Guid.NewGuid(), userId: Guid.NewGuid(),
                searchedData: "...", dateTime: DateTimeOffset.Now);

            return searchedArticleData;
        }

        private SearchedArticleDataDto GetSearchedArticleDataDto(SearchedArticleData searchedArticleData)
        {
            var searchedArticleDataDto = new SearchedArticleDataDto
            {
                SearchedArticleDataId = Guid.NewGuid(),
                ArticleId = searchedArticleData.ArticleID.Value.ToString(),
                UserId = searchedArticleData.UserID.Value.ToString(),
                SearchedData = searchedArticleData.SearchedData,
                DateTime = searchedArticleData.DateTime
            };

            return searchedArticleDataDto;
        }

        public RemoveSearchedArticleDataHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _searchedArticleDataReadService = Substitute.For<ISearchedArticleDataReadService>();
            _handler = new RemoveSearchedArticleDataHandler(_userRepository, _searchedArticleDataReadService);
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

        //Should throw SearchedArticleNotFoundException when the following condition is met:
        //There is no SearchedArticleData returned from the repository that corresponds to the SearchedArticleDataId from the RemoveSearchedArticleDataCommand.
        [Fact]
        public async Task Throws_SearchedArticleNotFoundException_When_SearchedArticleData_With_Given_SearchedArticleDataId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var removeSearchedArticleDataCommand = GetRemoveSearchedArticleDataCommand();

            _userRepository.GetUserByIdAsync(removeSearchedArticleDataCommand.UserId).Returns(user);

            _searchedArticleDataReadService.GetSearchedArticleDataByIdAsync(removeSearchedArticleDataCommand.SearchedArticleDataId)
               .Returns(default(SearchedArticleDataDto));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedArticleDataCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SearchedArticleNotFoundException>();
        }

        //Should remove SearchedArticleData value object if the UserId and SearchedArticleDataId from the RemoveSearchedArticleDataCommand
        //are valid Id's for an existing User domain entity and SearchedArticleData value object.
        //The repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_And_SearchedArticleDataId_Removes_SearchedArticleData_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var searchedArticleData = GetSearchedArticleData();

            var searchedArticleDataDto = GetSearchedArticleDataDto(searchedArticleData);

            var removeSearchedArticleDataCommand = GetRemoveSearchedArticleDataCommand();

            user.AddSearchedArticleData(searchedArticleData);

            _userRepository.GetUserByIdAsync(removeSearchedArticleDataCommand.UserId).Returns(user);

            _searchedArticleDataReadService.GetSearchedArticleDataByIdAsync(removeSearchedArticleDataCommand.SearchedArticleDataId)
                .Returns(searchedArticleDataDto);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeSearchedArticleDataCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
