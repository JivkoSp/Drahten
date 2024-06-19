using NSubstitute;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Commands.Handlers;
using PrivateHistoryService.Application.Exceptions;
using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.Repositories;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Application.Handlers
{
    public sealed class AddSearchedArticleDataHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddSearchedArticleDataCommand> _handler;

        private AddSearchedArticleDataCommand GetAddSearchedArticleDataCommand(Guid? userId = null)
        {
            var UserId = userId ?? Guid.NewGuid();

            var command = new AddSearchedArticleDataCommand(ArticleId: Guid.NewGuid(), UserId: UserId, 
               SearchedData: "...", SearchedDataAnswer: "......", SearchedDataAnswerContext: "......", DateTime: DateTimeOffset.Now);

            return command;
        }

        public AddSearchedArticleDataHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddSearchedArticleDataHandler(_userRepository);
        }

        #endregion

        private Task Act(AddSearchedArticleDataCommand command)
            => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addSearchedArticleDataCommand = GetAddSearchedArticleDataCommand();

            _userRepository.GetUserByIdAsync(addSearchedArticleDataCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addSearchedArticleDataCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create SearchedArticleData value object if the UserId from the AddSearchedArticleDataCommand is valid Id for existing User.
        //Additionally the created SearchedArticleData value object must be added to the User and the repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Creates_And_Adds_SearchedArticleData_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var addSearchedArticleDataCommand = GetAddSearchedArticleDataCommand(user.Id);

            _userRepository.GetUserByIdAsync(addSearchedArticleDataCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addSearchedArticleDataCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
