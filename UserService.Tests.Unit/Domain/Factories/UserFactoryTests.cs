using NSubstitute;
using Shouldly;
using System.Reflection;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;
using UserService.Domain.Factories;
using UserService.Domain.Factories.Interfaces;
using UserService.Domain.ValueObjects;
using Xunit;

namespace UserService.Tests.Unit.Domain.Factories
{
    public sealed class UserFactoryTests
    {
        #region GLOBAL ARRANGE

        private UserID Id;
        private UserFullName _userFullName;
        private UserNickName _userNickName;
        private UserEmailAddress _userEmailAddress;
        private readonly IUserFactory _userConcreteFactory;
        private readonly IUserFactory _userMockFactory;

        private User CreateUserWithReflection(UserID userId, UserFullName userFullName, UserNickName userNickName, 
            UserEmailAddress userEmailAddress)
        {
            //Retrieve a reference to the constructor of the User class that matches the specified parameters.
            var constructor = typeof(User).GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[]
                {
                    typeof(UserID),
                    typeof(UserFullName),
                    typeof(UserNickName),
                    typeof(UserEmailAddress)
                },
                null);

            //Invoke the retrieved constructor with the values that are provided when the CreateUserWithReflection is called.
            var user = (User)constructor.Invoke(new object[]
            {
                userId,
                userFullName,
                userNickName,
                userEmailAddress
            });

            return user;
        }

        public UserFactoryTests()
        {
            Id = new UserID(Guid.NewGuid());    
            _userFullName = new UserFullName("John Doe");
            _userNickName = new UserNickName("Johny");
            _userEmailAddress = new UserEmailAddress("johny@mail.com");
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
            var user = _userConcreteFactory.Create(Id, _userFullName, _userNickName, _userEmailAddress);

            //ASSERT
            user.ShouldNotBeNull();

            user.Id.ShouldBeSameAs(Id);

            //Comparing the user object field values with the provided values for the _userConcreteFactory.
            //--------------
            //This check is necessary because it ensures that the field values of the user object are the same as
            //the values that are provided to the concrete factory. 
            var userType = user.GetType();
            var fields = userType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                var fieldName = field.Name;

                //This fields are skipped becouse the fields that are important for the check are the fields that are set by
                //the Create method of the _userConcreteFactory.
                if (fieldName == "_bannedUsers" || fieldName == "_contactRequests" || fieldName == "_auditTrail")
                {
                    continue;
                }

                var expectedValue = GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);

                var actualValue = field.GetValue(user);

                actualValue.ShouldBe(expectedValue, $"Field {fieldName} should match the expected value!");
            }
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
            var user = _userConcreteFactory.Create(Id, _userFullName, _userNickName, _userEmailAddress);

            _userMockFactory.Create(Arg.Any<UserID>(), Arg.Any<UserFullName>(), Arg.Any<UserNickName>(), Arg.Any<UserEmailAddress>())
                .Returns(callInfo =>
                    {
                        var user = CreateUserWithReflection(callInfo.Arg<UserID>(), callInfo.Arg<UserFullName>(),
                            callInfo.Arg<UserNickName>(), callInfo.Arg<UserEmailAddress>());

                        return user;
                    });

            var userFromMockFactory = _userMockFactory.Create(Id, _userFullName, _userNickName, _userEmailAddress);

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
        public void Should_Throw_NullUserParameterException_When_One_Or_More_Null_Parameters_Are_Provided_In_User_Concrete_Factory()
        {
            //ACT
            var exception = Record.Exception(() => _userConcreteFactory.Create(Id, null, _userNickName, null));

            //ASSERT
            exception.ShouldNotBeNull();
            
            exception.ShouldBeOfType<NullUserParameterException>();
        }
    }
}
