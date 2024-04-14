using Shouldly;
using UserService.Domain.Entities;
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
    }
}
