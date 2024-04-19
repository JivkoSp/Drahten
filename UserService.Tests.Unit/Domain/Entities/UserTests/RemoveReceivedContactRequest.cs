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
    public sealed class RemoveReceivedContactRequest
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            return user;
        }

        private ContactRequest GetContactRequest()
        {
            var contactRequest = new ContactRequest(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            return contactRequest;
        }

        public RemoveReceivedContactRequest()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw ContactRequestNotFoundException when the following condition is met:
        //There is no contact request (ContactRequest value object) with matching UserID.
        [Fact]
        public void ContactRequest_NotFound_Throws_ContactRequestNotFoundException()
        {
            //ARRANGE
            var issuer = GetUser();

            var contactRequest = GetContactRequest();

            //ACT
            var exception = Record.Exception(() => issuer.RemoveIssuedContactRequest(contactRequest.ReceiverUserId));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ContactRequestNotFoundException>();
        }

        //Should remove received contact request (ContactRequest value object) from internal collection of ContactRequest value objects
        //and produce ContactRequestRemoved domain event.
        //The ContactRequestRemoved domain event should contain:
        //1. The same user entity that the contact request value object was removed from (The receiver).
        //2. The same contact request value object that was removed from the internal collection of ContactRequest value objects.
        [Fact]
        public void Removes_Received_ContactRequest_And_Produces_ContactRequestRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var receiver = GetUser();

            var contactRequest = GetContactRequest();

            receiver.ReceiveContactRequest(contactRequest);

            //ACT
            var exception = Record.Exception(() => receiver.RemoveReceivedContactRequest(contactRequest.IssuerUserId));

            //ASSERT
            exception.ShouldBeNull();

            receiver.DomainEvents.Count().ShouldBe(2);

            receiver.ReceivedContactRequests.Count().ShouldBe(0);

            var contactRequestAddedEvent = receiver.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(ContactRequestAdded)) as ContactRequestAdded;

            var contactRequestRemovedEvent = receiver.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(ContactRequestRemoved)) as ContactRequestRemoved;

            contactRequestAddedEvent.ShouldNotBeNull();

            contactRequestAddedEvent.User.ShouldBeSameAs(receiver);

            contactRequestAddedEvent.ContactRequest.ShouldBeSameAs(contactRequest);

            contactRequestRemovedEvent.ShouldNotBeNull();

            contactRequestRemovedEvent.User.ShouldBeSameAs(receiver);

            contactRequestRemovedEvent.ContactRequest.ShouldBeSameAs(contactRequest);
        }
    }
}
