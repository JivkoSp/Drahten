using Shouldly;
using UserService.Domain.Entities;
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

        
    }
}
