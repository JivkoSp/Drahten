using NSubstitute;
using Shouldly;
using TopicArticleService.Application.AsyncDataServices;
using TopicArticleService.Application.Commands;
using TopicArticleService.Application.Commands.Handlers;
using TopicArticleService.Application.Exceptions;
using TopicArticleService.Application.Services.ReadServices;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.Repositories;
using Xunit;

namespace TopicArticleService.Tests.Unit.Application.Handlers
{
    public class RegisterUserTopicHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly ITopicReadService _topicReadService;
        private readonly IUserRepository _userRepository;
        private readonly IMessageBusPublisher _messageBusPublisher;
        private readonly ICommandHandler<RegisterUserTopicCommand> _handler;

        private RegisterUserTopicCommand GetRegisterUserTopicCommand()
        {
            var registerUserTopicCommand = new RegisterUserTopicCommand(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);

            return registerUserTopicCommand;
        }

        public RegisterUserTopicHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _topicReadService = Substitute.For<ITopicReadService>();
            _userRepository = Substitute.For<IUserRepository>();
            _messageBusPublisher = Substitute.For<IMessageBusPublisher>();
            _handler = new RegisterUserTopicHandler(_topicReadService, _userRepository, _messageBusPublisher);
        }

        #endregion

        private Task Act(RegisterUserTopicCommand command)
            => _handler.HandleAsync(command);

        //Should throw TopicNotFoundException when the following condition is met:
        //There is no Topic returned from the topic read service.
        [Fact]
        public async Task Throws_TopicNotFoundException_When_Topic_Is_Not_Returned_From_TopicReadService()
        {
            //ARRANGE
            var registerUserTopicCommand = GetRegisterUserTopicCommand();

            _topicReadService.ExistsByIdAsync(registerUserTopicCommand.TopicId).Returns(false);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(registerUserTopicCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<TopicNotFoundException>();
        }

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var registerUserTopicCommand = GetRegisterUserTopicCommand();

            _topicReadService.ExistsByIdAsync(registerUserTopicCommand.TopicId).Returns(true);

            _userRepository.GetUserByIdAsync(registerUserTopicCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(registerUserTopicCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should create new UserTopic value object if the UserId and TopicId from the RegisterUserTopicCommand
        //are valid Ids for existing User and Topic domain entities.
        //Additionally the repository must be called one time to update the User entity.
        [Fact]
        public async Task Given_Valid_UserId_And_TopicId_Calls_UserRepository_On_Success()
        {
            //ARRANGE
            var registerUserTopicCommand = GetRegisterUserTopicCommand();

            var user = _userConcreteFactory.Create(Guid.NewGuid());

            _topicReadService.ExistsByIdAsync(registerUserTopicCommand.TopicId).Returns(true);

            _userRepository.GetUserByIdAsync(registerUserTopicCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(registerUserTopicCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
