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
    public sealed class RemoveTopicSubscription
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

        public RemoveTopicSubscription()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw TopicSubscriptionNotFoundException when the following condition is met:
        //There is no TopicSubscription value object with the same TopicID and UserID.
        [Fact]
        public void ViewedArticle_NotFound_Throws_ViewedArticleNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var topicSubscription = GetTopicSubscription();

            //ACT
            var exception = Record.Exception(() => user.RemoveTopicSubscription(topicSubscription.TopicID, topicSubscription.UserID));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<TopicSubscriptionNotFoundException>();
        }

        //Should remove TopicSubscription value object from internal collection of TopicSubscription value objects
        //and produce TopicSubscriptionRemoved domain event.
        //The TopicSubscriptionRemoved domain event should contain:
        //1. The same user entity that the TopicSubscription value object was removed from.
        //2. The same TopicSubscription value object that was removed from the internal collection of TopicSubscription value objects.
        [Fact]
        public void Removes_TopicSubscription_And_Produces_TopicSubscriptionRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var topicSubscription = GetTopicSubscription();

            user.AddTopicSubscription(topicSubscription);

            //ACT
            var exception = Record.Exception(() => user.RemoveTopicSubscription(topicSubscription.TopicID, topicSubscription.UserID));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.SubscribedTopics.Count().ShouldBe(0);

            var topicSubscriptionAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(TopicSubscriptionAdded)) as TopicSubscriptionAdded;

            var topicSubscriptionRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(TopicSubscriptionRemoved)) as TopicSubscriptionRemoved;

            topicSubscriptionAddedEvent.ShouldNotBeNull();

            topicSubscriptionAddedEvent.User.ShouldBeSameAs(user);

            topicSubscriptionAddedEvent.TopicSubscription.ShouldBeSameAs(topicSubscription);

            topicSubscriptionRemovedEvent.ShouldNotBeNull();

            topicSubscriptionRemovedEvent.User.ShouldBeSameAs(user);

            topicSubscriptionRemovedEvent.TopicSubscription.ShouldBeSameAs(topicSubscription);
        }
    }
}
