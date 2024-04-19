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
    public class IssueContactRequest
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

        public IssueContactRequest()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw ContactRequestAlreadyExistsException when the following condition is met:
        //There is already contact request (ContactRequest value object) with matching UserID.
        [Fact]
        public void Duplicate_ContactRequest_Throws_ContactRequestAlreadyExistsException()
        {
            //ARRANGE
            var issuer = GetUser();

            var contactRequest = GetContactRequest();

            issuer.IssueContactRequest(contactRequest);

            //ACT
            var exception = Record.Exception(() => issuer.IssueContactRequest(contactRequest));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<ContactRequestAlreadyExistsException>();
        }

        //Should add contact request (ContactRequest value object) to internal collection of ContactRequest value objects
        //and produce ContactRequestAdded domain event.
        //*** IMPORTANT! *** - The contact request should be added to the ISSUED contact request collection. 
        //The ContactRequestAdded domain event should contain:
        //1. The same user entity that the contact request was added to (The issuer).
        //2. The same contact request value object that was added to the internal collection of ContactRequest value objects.
        [Fact]
        public void Adds_ContactRequest_And_Produces_ContactRequestAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var issuer = GetUser();

            var contactRequest = GetContactRequest(); //contact request from the issuer (another User).

            //ACT
            var exception = Record.Exception(() => issuer.IssueContactRequest(contactRequest));

            //ASSERT
            exception.ShouldBeNull();

            issuer.DomainEvents.Count().ShouldBe(1);

            issuer.IssuedContactRequests.Count().ShouldBe(1);

            issuer.ReceivedContactRequests.Count().ShouldBe(0);

            var contactRequestAddedEvent = issuer.DomainEvents.FirstOrDefault() as ContactRequestAdded;

            contactRequestAddedEvent.ShouldNotBeNull();

            contactRequestAddedEvent.User.ShouldBeSameAs(issuer);

            contactRequestAddedEvent.ContactRequest.ShouldBeSameAs(contactRequest);
        }
    }
}
