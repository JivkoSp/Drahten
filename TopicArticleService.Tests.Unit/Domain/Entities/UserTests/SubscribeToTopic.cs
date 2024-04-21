
using Shouldly;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Domain.Events;
using TopicArticleService.Domain.Exceptions;
using TopicArticleService.Domain.Factories;
using TopicArticleService.Domain.ValueObjects;
using Xunit;

namespace TopicArticleService.Tests.Unit.Domain.Entities.UserTests
{
    public class SubscribeToTopic
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userConcreteFactory;
        private readonly ITopicFactory _topicConcreteFactory;

        private User GetUser()
        {
            var user = _userConcreteFactory.Create(Guid.NewGuid());

            user.ClearEvents();

            return user;
        }

        private Topic GetTopic()
        {
            var topic = _topicConcreteFactory.Create(Guid.NewGuid(), "topic1");

            return topic;
        }

        public SubscribeToTopic()
        {
            _userConcreteFactory = new UserFactory();
            _topicConcreteFactory = new TopicFactory();
        }

        #endregion

        //Should throw UserTopicAlreadyExistsException when the following condition is met:
        //There is already UserTopic value object with matching TopicId.
        [Fact]
        public void Duplicate_UserTopic_Throws_UserTopicAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var topic = GetTopic();

            var userTopic = new UserTopic(user.Id, topic.Id, DateTimeOffset.Now);

            user.SubscribeToTopic(userTopic);

            //ACT
            var exception = Record.Exception(() => user.SubscribeToTopic(userTopic));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserTopicAlreadyExistsException>();
        }

        //Should add UserTopic value object to internal collection of UserTopic value objects
        //and produce UserTopicAdded domain event.
        //The UserTopicAdded domain event should contain:
        //1. The same user entity to which the UserTopic value object was added.
        //2. The same UserTopic value object that was added to the internal collection of UserTopic value objects.
        [Fact]
        public void Adds_UserTopic_And_Produces_UserTopicAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var topic = GetTopic();

            var userTopic = new UserTopic(user.Id, topic.Id, DateTimeOffset.Now);

            //ACT
            var exception = Record.Exception(() => user.SubscribeToTopic(userTopic));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.SubscribedTopics.Count().ShouldBe(1);

            var userTopicAddedEvent = user.DomainEvents.FirstOrDefault() as UserTopicAdded;

            userTopicAddedEvent.ShouldNotBeNull();

            userTopicAddedEvent.User.ShouldBeSameAs(user);

            userTopicAddedEvent.UserTopic.ShouldBeSameAs(userTopic);
        }
    }
}
