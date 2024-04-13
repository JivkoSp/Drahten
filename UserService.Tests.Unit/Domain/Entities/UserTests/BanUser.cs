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
    public sealed class BanUser
    {
        #region GLOBAL ARRANGE

        private readonly IUserFactory _userFactory;
        private readonly IBannedUserFactory _bannedUserFactory;

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            return user;
        }

        private BannedUser GetBannedUser()
        {
            var bannedUser = _bannedUserFactory.Create(Guid.NewGuid());

            return bannedUser;
        }

        public BanUser()
        {
            _userFactory = new UserFactory();
            _bannedUserFactory = new BannedUserFactory();
        }

        #endregion

        //Should throw BannedUserAlreadyExistsException when the following condition is met:
        //There is already banned user (BannedUser value object) with matching UserID.
        [Fact]
        public void Duplicate_BannedUser_Throws_BannedUserAlreadyExistsException()
        {
            //ARRANGE
            var user = GetUser();

            var bannedUser = GetBannedUser();

            user.BanUser(bannedUser);

            //ACT
            var exception = Record.Exception(() => user.BanUser(bannedUser));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<BannedUserAlreadyExistsException>();
        }

        //Should add banned user (BannedUser value object) to internal collection of BannedUser value objects
        //and produce BannedUserAdded domain event.
        //The BannedUserAdded domain event should contain:
        //1. The same user entity that the banned user was added to.
        //2. The same banned user value object that was added to the internal collection of BannedUser value objects.
        [Fact]
        public void Adds_BannedUser_And_Produces_BannedUserAdded_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var bannedUser = GetBannedUser();

            //ACT
            var exception = Record.Exception(() => user.BanUser(bannedUser));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(1);

            user.BannedUsers.Count().ShouldBe(1);

            var bannedUserAddedEvent = user.DomainEvents.FirstOrDefault() as BannedUserAdded;

            bannedUserAddedEvent.ShouldNotBeNull();

            bannedUserAddedEvent.User.ShouldBeSameAs(user);

            bannedUserAddedEvent.BannedUser.ShouldBeSameAs(bannedUser);
        }
    }
}
