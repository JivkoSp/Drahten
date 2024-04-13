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
    public sealed class AddContactRequest
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

        public AddContactRequest()
        {
            _userFactory = new UserFactory();
            _contactRequestFactory = new ContactRequestFactory();
        }

        #endregion

        //Should throw ContactRequestAlreadyExistsException when the following condition is met:
        //There is already contact request (ContactRequest value object) with matching UserID.
        [Fact]
        public void Duplicate_ContactRequest_Throws_ContactRequestAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var contactRequest = GetContactRequest();

            user.AddContactRequest(contactRequest);

            //ACT
            var exception = Record.Exception(() => user.AddContactRequest(contactRequest));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ContactRequestAlreadyExistsException>();
        }

        //Should add contact request (ContactRequest value object) to internal collection of ContactRequest value objects
        //and produce ContactRequestAdded domain event.
        //The ContactRequestAdded domain event should contain:
        //1. The same user entity that the contact request was added to.
        //2. The same contact request value object that was added to the internal collection of ContactRequest value objects.
        [Fact]
        public void Adds_ContactRequest_And_Produces_ContactRequestAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var contactRequest = GetContactRequest();

            //ACT
            var exception = Record.Exception(() => user.AddContactRequest(contactRequest));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.ContactRequests.Count().ShouldBe(1);

            var contactRequestAddedEvent = user.DomainEvents.FirstOrDefault() as ContactRequestAdded;

            contactRequestAddedEvent.ShouldNotBeNull();

            contactRequestAddedEvent.User.ShouldBeSameAs(user);

            contactRequestAddedEvent.ContactRequest.ShouldBeSameAs(contactRequest);
        }
    }
}
