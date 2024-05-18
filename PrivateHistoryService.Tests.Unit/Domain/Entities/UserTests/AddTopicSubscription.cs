using PrivateHistoryService.Domain.Entities;
using PrivateHistoryService.Domain.Events;
using PrivateHistoryService.Domain.Exceptions;
using PrivateHistoryService.Domain.Factories;
using PrivateHistoryService.Domain.Factories.Interfaces;
using PrivateHistoryService.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace PrivateHistoryService.Tests.Unit.Domain.Entities.UserTests
{
    public sealed class AddTopicSubscription
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid());

            return user;
        }

        private TopicSubscription GetTopicSubscription()
        {
            var topicSubscription = new TopicSubscription(topicId: Guid.NewGuid(), userId: Guid.NewGuid(), dateTime: DateTimeOffset.Now);

            return topicSubscription;
        }

        public AddTopicSubscription()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw SubscribedTopicAlreadyExistsException when the following condition is met:
        //Another TopicSubscription value object with the same TopicID and UserID already exists.
        [Fact]
        public void Duplicate_TopicSubscription_Throws_SubscribedTopicAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var topicSubscription = GetTopicSubscription();

            user.AddTopicSubscription(topicSubscription);

            //ACT
            var exception = Record.Exception(() => user.AddTopicSubscription(topicSubscription));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<SubscribedTopicAlreadyExistsException>();
        }

        //Should add TopicSubscription value object to internal collection of TopicSubscription value objects
        //and produce TopicSubscriptionAdded domain event.
        //The TopicSubscriptionAdded domain event should contain:
        //1. The same user entity to which the TopicSubscription value object was added.
        //2. The same TopicSubscription value object that was added to the internal collection of TopicSubscription value objects.
        [Fact]
        public void Adds_TopicSubscription_And_Produces_TopicSubscriptionAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var topicSubscription = GetTopicSubscription();

            //ACT
            var exception = Record.Exception(() => user.AddTopicSubscription(topicSubscription));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.SubscribedTopics.Count().ShouldBe(1);

            var topicSubscriptionAddedEvent = user.DomainEvents.FirstOrDefault() as TopicSubscriptionAdded;

            topicSubscriptionAddedEvent.ShouldNotBeNull();

            topicSubscriptionAddedEvent.User.ShouldBeSameAs(user);

            topicSubscriptionAddedEvent.TopicSubscription.ShouldBeSameAs(topicSubscription);
        }
    }
}
