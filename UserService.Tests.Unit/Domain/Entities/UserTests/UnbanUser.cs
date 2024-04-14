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
    public sealed class UnbanUser
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

        public UnbanUser()
        {
            _userFactory = new UserFactory();
            _bannedUserFactory = new BannedUserFactory();
        }

        #endregion

        //Should throw BannedUserNotFoundException when the following condition is met:
        //There is no banned user (BannedUser value object) with matching UserID.
        [Fact]
        public void BannedUser_NotFound_Throws_BannedUserNotFoundException()
        {
            //ARRANGE
            var user = GetUser();

            var bannedUser = GetBannedUser();

            //ACT
            var exception = Record.Exception(() => user.UnbanUser(bannedUser));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<BannedUserNotFoundException>();
        }

        //Should remove banned user (BannedUser value object) from internal collection of BannedUser value objects
        //and produce BannedUserRemoved domain event.
        //The BannedUserRemoved domain event should contain:
        //1. The same user entity that the banned user value object was removed from.
        //2. The same banned user value object that was removed from the internal collection of BannedUser value objects.
        [Fact]
        public void Removes_BannedUser_And_Produces_BannedUserRemoved_Domain_Event_On_Success()
        {
            //ARRANGE
            var user = GetUser();

            var bannedUser = GetBannedUser();

            user.BanUser(bannedUser);

            //ACT
            var exception = Record.Exception(() => user.UnbanUser(bannedUser));

            //ASSERT
            exception.ShouldBeNull();

            user.DomainEvents.Count().ShouldBe(2);

            user.BannedUsers.Count().ShouldBe(0);

            var bannedUserAddedEvent = user.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(BannedUserAdded)) as BannedUserAdded;

            var bannedUserRemovedEvent = user.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(BannedUserRemoved)) as BannedUserRemoved;

            bannedUserAddedEvent.ShouldNotBeNull();

            bannedUserAddedEvent.User.ShouldBeSameAs(user);

            bannedUserAddedEvent.BannedUser.ShouldBeSameAs(bannedUser);

            bannedUserRemovedEvent.ShouldNotBeNull();

            bannedUserRemovedEvent.User.ShouldBeSameAs(user);

            bannedUserRemovedEvent.BannedUser.ShouldBeSameAs(bannedUser);
        }
    }
}
