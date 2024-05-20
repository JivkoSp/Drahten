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
    public sealed class AddDislikedArticleCommentHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<AddDislikedArticleCommentCommand> _handler;

        private AddDislikedArticleCommentCommand GetAddDislikedArticleCommentCommand(Guid? userId = null)
        {
            var UserId = userId ?? Guid.NewGuid();

            var command = new AddDislikedArticleCommentCommand(ArticleId: Guid.NewGuid(), UserId: UserId,
                ArticleCommentId: Guid.NewGuid(), DateTime: DateTimeOffset.Now);

            return command;
        }

        public AddDislikedArticleCommentHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new AddDislikedArticleCommentHandler(_userRepository);
        }

        #endregion

        private Task Act(AddDislikedArticleCommentCommand command)
         => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var addDislikedArticleCommentCommand = GetAddDislikedArticleCommentCommand();

            _userRepository.GetUserByIdAsync(addDislikedArticleCommentCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addDislikedArticleCommentCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create DislikedArticleComment value object if the UserId from the AddDislikedArticleCommentCommand is valid Id for existing User.
        //Additionally the created DislikedArticleComment value object must be added to the User and the repository must be called to update the User.
        [Fact]
        public async Task Given_Valid_UserId_Creates_And_Adds_DislikedArticleComment_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var addDislikedArticleCommentCommand = GetAddDislikedArticleCommentCommand(user.Id);

            _userRepository.GetUserByIdAsync(addDislikedArticleCommentCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(addDislikedArticleCommentCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
