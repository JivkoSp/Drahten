using Shouldly;
using UserService.Domain.Entities;
using UserService.Domain.Events;
using UserService.Domain.Exceptions;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;
using Xunit;

namespace UserService.Tests.Unit.Domain.Entities.UserTests
{
    public sealed class AddToAuditTrail
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            return user;
        }

        private UserTracking GetUserTracking()
        {
            var userTracking = new UserTracking(Guid.NewGuid(), "LogIn", DateTimeOffset.Now, "some link");

            return userTracking;
        }

        public AddToAuditTrail()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw UserTrackingAlreadyExistsException when the following condition is met:
        //The audit trail already has the same UserTracking value object.
        [Fact]
        public void Duplicate_UserTracking_Throws_UserTrackingAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var userTracking = GetUserTracking();

            user.AddToAuditTrail(userTracking);

            //ACT
            var exception = Record.Exception(() => user.AddToAuditTrail(userTracking));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<UserTrackingAlreadyExistsException>();
        }

        //Should add user tracking (UserTracking value object) to internal collection of UserTracking value objects
        //and produce UserTrackingAuditAdded domain event.
        //The UserTrackingAuditAdded domain event should contain:
        //1. The same user entity to which the user tracking value object was added.
        //2. The same user tracking value object that was added to the internal collection of UserTracking value objects.
        [Fact]
        public void Adds_UserTracking_And_Produces_UserTrackingAuditAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var userTracking = GetUserTracking();

            //ACT
            var exception = Record.Exception(() => user.AddToAuditTrail(userTracking));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.AuditTrail.Count().ShouldBe(1);

            var userTrackingAuditAddedEvent = user.DomainEvents.FirstOrDefault() as UserTrackingAuditAdded;

            userTrackingAuditAddedEvent.ShouldNotBeNull();

            userTrackingAuditAddedEvent.User.ShouldBeSameAs(user);

            userTrackingAuditAddedEvent.UserTracking.ShouldBeSameAs(userTracking);
        }
    }
}
