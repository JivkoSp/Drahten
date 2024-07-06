using NSubstitute;
using PublicHistoryService.Domain.Entities;
using PublicHistoryService.Domain.Exceptions;
using PublicHistoryService.Domain.Factories;
using PublicHistoryService.Domain.Factories.Interfaces;
using PublicHistoryService.Domain.ValueObjects;
using Shouldly;
using System.Reflection;
using Xunit;

namespace PublicHistoryService.Tests.Unit.Domain.Factories
{
    public sealed class UserFactoryTests
    {
        #region GLOBAL ARRANGE

        private UserID Id;
        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserFactory _userMockFactory;

        private User CreateUserWithReflection(UserID userId)
        {
            //Retrieve a reference to the constructor of the User class that matches the specified parameters.
            var constructor = typeof(User).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(UserID) }, null);

            //Invoke the retrieved constructor with the values that are provided when the CreateUserWithReflection is called.
            var user = (User)constructor.Invoke(new object[] { userId });

            return user;
        }

        public UserFactoryTests()
        {
            Id = new UserID(Guid.NewGuid());
            _userConcreteFactory = new UserFactory();
            _userMockFactory = Substitute.For<IUserFactory>();
        }

        #endregion

        //Should create User instance from concrete factory implementation. The created User instance should
        //have the same values as the values provided to the concrete factory.
        [Fact]
        public void Given_Valid_User_Parameters_Should_Create_User_Instance_From_Concrete_Factory()
        {
            //ACT
            var user = _userConcreteFactory.Create(Id);

            //ASSERT
            user.ShouldNotBeNull();

            user.Id.ShouldBeSameAs(Id);
        }

        //Should create two user instances with equal values when equal values are given to the concrete factory (_userConcreteFactory)
        //and to the mock factory (_userMockFactory).
        //--------------
        //This test ensures that both factories produce equivalent User instances when provided with the same input parameters,
        //thus validating the correctness of the concrete factory implementation.
        //The mock factory sets the way that user instance should be created. 
        //This is needed becouse the concrete factory may not create user instance as expected (for example it may use different constructor).
        [Fact]
        public void Should_Create_Equal_User_Instances_From_Concrete_And_Mock_Factories()
        {
            //ACT
            var user = _userConcreteFactory.Create(Id);

            _userMockFactory.Create(Arg.Any<UserID>())
                .Returns(callInfo =>
                {
                    var user = CreateUserWithReflection(callInfo.Arg<UserID>());

                    return user;
                });

            var userFromMockFactory = _userMockFactory.Create(Id);

            //ASSERT
            userFromMockFactory.ShouldNotBeNull();

            //Comparing the values of the user object that is created by _userConcreteFactory with the values of
            //the userFromMockFactory object that is created by _userMockFactory. 
            var userType = user.GetType();
            var fields = userType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var valueFromUserConcreteImplementation = field.GetValue(user);

                var valueFromUserMockImplementation = field.GetValue(userFromMockFactory);

                valueFromUserConcreteImplementation.ShouldBe(valueFromUserMockImplementation,
                    $"Field {field.Name} should be equal!");
            }
        }

        [Fact]
        public void Should_Throw_NullUserParametersException_When_One_Or_More_Null_Parameters_Are_Provided_In_User_Concrete_Factory()
        {
            //ACT
            var exception = Record.Exception(() => _userConcreteFactory.Create(null));

            //ASSERT
            exception.ShouldNotBeNull();

            exception.ShouldBeOfType<NullUserParametersException>();
        }
    }
}
