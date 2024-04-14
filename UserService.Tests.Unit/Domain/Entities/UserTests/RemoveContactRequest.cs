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
    public sealed class RemoveContactRequest
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;
        private readonly IContactRequestFactory _contactRequestFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            return user;
        }

        private ContactRequest GetContactRequest()
        {
            var contactRequest = _contactRequestFactory.Create(Guid.NewGuid(), DateTimeOffset.Now);

            return contactRequest;
        }

        public RemoveContactRequest()
        {
            _userFactory = new UserFactory();
            _contactRequestFactory = new ContactRequestFactory();
        }

        #endregion

        //Should throw ContactRequestNotFoundException when the following condition is met:
        //There is no contact request (ContactRequest value object) with matching UserID.
        [Fact]
        public void ContactRequest_NotFound_Throws_ContactRequestNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var contactRequest = GetContactRequest();

            //ACT
            var exception = Record.Exception(() => user.RemoveContactRequest(contactRequest));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ContactRequestNotFoundException>();
        }

        //Should remove contact request (ContactRequest value object) from internal collection of ContactRequest value objects
        //and produce ContactRequestRemoved domain event.
        //The ContactRequestRemoved domain event should contain:
        //1. The same user entity that the contact request value object was removed from.
        //2. The same contact request value object that was removed from the internal collection of ContactRequest value objects.
        [Fact]
        public void Removes_ContactRequest_And_Produces_ContactRequestRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var contactRequest = GetContactRequest();

            user.AddContactRequest(contactRequest);

            //ACT
            var exception = Record.Exception(() => user.RemoveContactRequest(contactRequest));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.ContactRequests.Count().ShouldBe(0);

            var contactRequestAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ContactRequestAdded)) as ContactRequestAdded;

            var contactRequestRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ContactRequestRemoved)) as ContactRequestRemoved;

            contactRequestAddedEvent.ShouldNotBeNull();

            contactRequestAddedEvent.User.ShouldBeSameAs(user);

            contactRequestAddedEvent.ContactRequest.ShouldBeSameAs(contactRequest);

            contactRequestRemovedEvent.ShouldNotBeNull();

            contactRequestRemovedEvent.User.ShouldBeSameAs(user);

            contactRequestRemovedEvent.ContactRequest.ShouldBeSameAs(contactRequest);
        }
    }
}
