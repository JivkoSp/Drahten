using NSubstitute;
using Shouldly;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;
using Xunit;

namespace UserService.Tests.Unit.Domain.Factories
{
    public sealed class BannedUserFactoryTests
    {
        #region GLOBAL ARRANGE

        private UserID _userId;
        private readonly IBannedUserFactory _bannedUserConcreteFactory;
        private readonly IBannedUserFactory _bannedUserMockFactory;

        public BannedUserFactoryTests()
        {
            _userId = new UserID(Guid.NewGuid());
            _bannedUserConcreteFactory = new BannedUserFactory();
            _bannedUserMockFactory = Substitute.For<IBannedUserFactory>();
        }

        #endregion

        //Should create BannedUser instance from concrete factory implementation. The created BannedUser instance should
        //have the same values as the values provided to the concrete factory.
        [Fact]
        public void Given_Valid_BannedUser_Parameters_Should_Create_BannedUser_Instance_From_Concrete_Factory()
        {
            //ACT
            var bannedUser = _bannedUserConcreteFactory.Create(_userId);

            //ASSERT
            bannedUser.ShouldNotBeNull();

            bannedUser.UserId.ShouldBe(_userId);
        }

        //Should create two BannedUser instances with equal values when equal values are given to the concrete factory
        //(_bannedUserConcreteFactory) and to the mock factory (_bannedUserMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent BannedUser instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that BannedUser instance should be created. 
        //This is needed becouse the concrete factory may not create BannedUser instance as expected
        //(for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_BannedUser_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var bannedUser = _bannedUserConcreteFactory.Create(_userId);

            _bannedUserMockFactory.Create(Arg.Any<UserID>()).Returns(
                callInfo =>
                {
                    var bannedUser = new BannedUser(callInfo.ArgAt<UserID>(0));

                    return bannedUser;
                });

            var bannedUserFromMockFactory = _bannedUserMockFactory.Create(_userId);

            //ASSERT
            bannedUserFromMockFactory.ShouldNotBeNull();

            bannedUser.UserId.ShouldBe(bannedUserFromMockFactory.UserId);
        }
    }
}
