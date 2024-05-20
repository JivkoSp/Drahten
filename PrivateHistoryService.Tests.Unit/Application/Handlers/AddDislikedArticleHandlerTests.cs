
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
    public sealed class AddDislikedArticleHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddDislikedArticleCommand> _handler;

        private AddDislikedArticleCommand GetAddDislikedArticleCommand(Guid? userId = null)
        {
            var UserId = userId ?? Guid.NewGuid();

            var command = new AddDislikedArticleCommand(ArticleId: Guid.NewGuid(), UserId: UserId, DateTime: DateTimeOffset.Now);

            return command;
        }

        public AddDislikedArticleHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddDislikedArticleHandler(_userRepository);
        }

        #endregion

        private Task Act(AddDislikedArticleCommand command)
         => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addDislikedArticleCommand = GetAddDislikedArticleCommand();

            _userRepository.GetUserByIdAsync(addDislikedArticleCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addDislikedArticleCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create DislikedArticle value object if the UserId from the AddDislikedArticleCommand is valid Id for existing User.
        //Additionally the created DislikedArticle value object must be added to the User and the repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Creates_And_Adds_DislikedArticle_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var addDislikedArticleCommand = GetAddDislikedArticleCommand(user.Id);

            _userRepository.GetUserByIdAsync(addDislikedArticleCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addDislikedArticleCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
