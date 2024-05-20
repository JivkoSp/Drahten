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
    public sealed class RemoveTopicSubscriptionHandlerTests
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserRepository _userRepository;
        private readonly ICommandHandler<RemoveTopicSubscriptionCommand> _handler;

        private RemoveTopicSubscriptionCommand GetRemoveTopicSubscriptionCommand(Guid? topicId = null, Guid? userId = null)
        {
            var TopicId = topicId ?? Guid.NewGuid();
            var UserId = userId ?? Guid.NewGuid();

            var command = new RemoveTopicSubscriptionCommand(TopicId: TopicId, UserId: UserId);

            return command;
        }

        private TopicSubscription GetTopicSubscription()
        {
            var topicSubscription = new TopicSubscription(topicId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return topicSubscription;
        }

        public RemoveTopicSubscriptionHandlerTests()
        {
            _userConcreteFactory = new UserFactory();
            _userRepository = Substitute.For<IUserRepository>();
            _handler = new RemoveTopicSubscriptionHandler(_userRepository);
        }

        #endregion

        private Task Act(RemoveTopicSubscriptionCommand command)
           => _handler.HandleAsync(command);

        //Should throw UserNotFoundException when the following condition is met:
        //There is no User returned from the repository that corresponds to the UserId from the command.
        [Fact]
        public async Task Throws_UserNotFoundException_When_User_With_Given_UserId_Is_Not_Returned_From_Repository()
        {
            //ARRANGE
            var removeTopicSubscriptionCommand = GetRemoveTopicSubscriptionCommand();

            _userRepository.GetUserByIdAsync(removeTopicSubscriptionCommand.UserId).Returns(default(User));

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeTopicSubscriptionCommand));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserNotFoundException>();
        }

        //Should remove TopicSubscription value object if the TopicId and UserId from the RemoveSearchedTopicDataCommand are valid Ids
        //for an existing Topic and User.
        //Ensure the repository is called to update the User.
        [Fact]
        public async Task Given_Valid_TopicId_And_UserId_Removes_TopicSubscription_Instance_From_User_And_Calls_Repository_On_Success()
        {
            //ARRANGE
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            var topicSubscription = GetTopicSubscription();

            var removeTopicSubscriptionCommand = GetRemoveTopicSubscriptionCommand(topicSubscription.TopicID, topicSubscription.UserID);

            user.AddTopicSubscription(topicSubscription);

            _userRepository.GetUserByIdAsync(removeTopicSubscriptionCommand.UserId).Returns(user);

            //ACT
            var exception = await Record.ExceptionAsync(async () => await Act(removeTopicSubscriptionCommand));

            //ASSERT
            exception.ShouldBeNull();

            await _userRepository.Received(1).UpdateUserAsync(user);
        }
    }
}
