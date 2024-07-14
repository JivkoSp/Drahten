using NSubstitute;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Application.Services.WriteServices;
using Xunit;
using Shouldly;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Domain.Repositories;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Entities;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class RegisterUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserReadService _userReadService;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly ICommandHandler<RegisterUserCommand> _handler;

        private RegisterUserCommand GetRegisterUserCommand()
        {
            var command = new RegisterUserCommand(Guid.NewGuid());

            return command;
        }

        public RegisterUserHandlerTests()
        {
            _userReadService = Substitute.For<IUserReadService>();
            _userRepository = Substitute.For<IUserRepository>();
            _userFactory = Substitute.For<IUserFactory>();
            _handler = new RegisterUserHandler(_userReadService, _userRepository, _userFactory);
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

        //Should create new User if there is NOT already user with the same UserId as the UserId
        //from the RegisterUserCommand (e.g If the UserId from the RegisterUserCommand is valid).
        //--------------------------------------------------------------------------
        //The user write service (IUserWriteService) must be called one time.
        [Fact]
        public async Task GivenValidUserId_Calls_UserWriteService_On_Success()
        {
            //ARRANGE
            var command = GetRegisterUserCommand();

            _userReadService.ExistsByIdAsync(command.UserId).Returns(false);

            _userFactory.Create(command.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(command));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).AddUserAsync(default(User));
        }
    }
}
