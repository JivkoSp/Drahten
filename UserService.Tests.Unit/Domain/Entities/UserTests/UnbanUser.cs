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

        private User GetUser()
        {
            var user = _userFactory.Create(Guid.NewGuid(), "John Doe", "Johny", "johny@mail.com");

            return user;
        }

        private BannedUser GetBannedUser()
        {
            var bannedUser = new BannedUser(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);

            return bannedUser;
        }

        public UnbanUser()
        {
            _userFactory = new UserFactory();
        }

        #endregion

        //Should throw BannedUserNotFoundException when the following condition is met:
        //There is no banned user (BannedUser value object) with matching UserID.
        [Fact]
        public void BannedUser_NotFound_Throws_BannedUserNotFoundException()
        {
            //ARRANGE
            var issuer = GetUser();

            var bannedUser = GetBannedUser();

            //ACT
            var exception = Record.Exception(() => issuer.UnbanUser(bannedUser.ReceiverUserId));

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
            var issuer = GetUser();

            var bannedUser = GetBannedUser();

            issuer.BanUser(bannedUser);

            //ACT
            var exception = Record.Exception(() => issuer.UnbanUser(bannedUser.ReceiverUserId));

            //ASSERT
            exception.ShouldBeNull();

            issuer.DomainEvents.Count().ShouldBe(2);

            issuer.IssuedUserBans.Count().ShouldBe(0);

            var bannedUserAddedEvent = issuer.DomainEvents.FirstOrDefault(x =>
                 x.GetType() == typeof(BannedUserAdded)) as BannedUserAdded;

            var bannedUserRemovedEvent = issuer.DomainEvents.FirstOrDefault(x =>
                x.GetType() == typeof(BannedUserRemoved)) as BannedUserRemoved;

            bannedUserAddedEvent.ShouldNotBeNull();

            bannedUserAddedEvent.User.ShouldBeSameAs(issuer);

            bannedUserAddedEvent.BannedUser.ShouldBeSameAs(bannedUser);

            bannedUserRemovedEvent.ShouldNotBeNull();

            bannedUserRemovedEvent.User.ShouldBeSameAs(issuer);

            bannedUserRemovedEvent.BannedUser.ShouldBeSameAs(bannedUser);
        }
    }
}
