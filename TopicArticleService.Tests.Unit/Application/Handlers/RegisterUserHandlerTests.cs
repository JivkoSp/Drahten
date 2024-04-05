using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Application.Services.WriteServices;
using Xunit;
using Shouldly;
using TopicArticleService.Application.Exceptions;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class RegisterUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserReadService _userReadService;
        private readonly IUserWriteService _userWriteService;
        private readonly ICommandHandler<RegisterUserCommand> _handler;

        private RegisterUserCommand GetRegisterUserCommand()
        {
            var command = new RegisterUserCommand(Guid.NewGuid());

            return command;
        }

        public RegisterUserHandlerTests()
        {
            _userReadService = Substitute.For<IUserReadService>();
            _userWriteService = Substitute.For<IUserWriteService>();
            _handler = new RegisterUserHandler(_userReadService, _userWriteService);
        }

        #endregion

        private Task Act(RegisterUserCommand command)
            => _handler.HandleAsync(command);

        //Should throw UserAlreadyExistsException when the following condition is met:
        //There is already user with the same UserId as the UserId from the RegisterUserCommand.
        [Fact]
        public async Task DuplicateUser_Throws_UserAlreadyExistsException()
        {
            //ARRANGE
            var command = GetRegisterUserCommand();

            _userReadService.ExistsByIdAsync(command.UserId).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(command));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserAlreadyExistsException>();
        }
    }
}
