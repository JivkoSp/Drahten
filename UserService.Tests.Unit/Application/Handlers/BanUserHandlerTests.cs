using NSubstitute;
using UserService.Application.Commands.Handlers;
using UserService.Application.Commands;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.Factories;
using UserService.Domain.Repositories;
using UserService.Application.Services.ReadServices;
using Xunit;
using UserService.Domain.Entities;
using Shouldly;
using UserService.Application.Exceptions;

namespace UserService.Tests.Unit.Application.Handlers
{
    public class BanUserHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly IUserReadService _userReadService;
        private readonly ICommandHandler<BanUserCommand> _handler;

        private BanUserCommand GetBanUserCommand()
        {
            var command = new BanUserCommand(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);

            return command;
        }

        public BanUserHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _userReadService = Substitute.For<IUserReadService>();
            _handler = new BanUserHandler(_userRepository, _userReadService);
        }

        #endregion

        private Task Act(BanUserCommand command)
          => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the IssuerUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_IssuerUserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var banUserCommand = GetBanUserCommand();

            _userRepository.GetUserByIdAsync(banUserCommand.IssuerUserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(banUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the read service that corresponds to the ReceiverUserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_ReceiverUserId_Is_Not_Returned_From_ReadService()
        {
            //ARRANGE
            var banUserCommand = GetBanUserCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(banUserCommand.IssuerUserId).Returns(user);

            _userReadService.ExistsByIdAsync(banUserCommand.ReceiverUserId).Returns(false);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(banUserCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create BannedUser value object if the IssuerUserId and ReceiverUserId from the BanUserCommand
        //are valid Ids for existing User domain entities.
        //Additionally the created BannedUser value object must be added to the User domain entity that corresponds to the issuer
        //side of the ContactRequest and the repository must be called to update that user entity.
        [Fact]
        public async Task Given_Valid_IssuerUserId_And_ReceiverUserId_Creates_And_Adds_BannedUser_Instance_To_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var banUserCommand = GetBanUserCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            _userRepository.GetUserByIdAsync(banUserCommand.IssuerUserId).Returns(user);

            _userReadService.ExistsByIdAsync(banUserCommand.ReceiverUserId).Returns(true);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(banUserCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
