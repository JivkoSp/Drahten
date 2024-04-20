using Newtonsoft.Json;
using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class RegisterUserTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        public RegisterUserTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if User is created from the specified parameters in CreateUserCommand.
        [Fact]
        public async Task Register_User_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var createUserCommand = new CreateUserCommand(Guid.NewGuid(), "John Doe", "Johny", "john@mail.com");

            //ACT
            var response = await Post(createUserCommand, "/user-service/users");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/user-service/users");
        }

        //Should return http status code 200 when the previously created User is successfully received from the database.
        [Fact]
        public async Task Register_User_Endpoint_Should_Add_User_With_Given_UserId_To_The_Database()
        {
            //ARRANGE
            var createUserCommand = new CreateUserCommand(Guid.NewGuid(), "John Doe", "Johny", "john@mail.com");

            //ACT
            await Post(createUserCommand, "/user-service/users");

            var response = await Get($"/user-service/users/{createUserCommand.UserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var userDto = JsonConvert.DeserializeObject<UserDto>(responseSerializedContent);

            userDto.ShouldNotBeNull();

            userDto.UserId.ShouldBe(createUserCommand.UserId.ToString());

            userDto.UserFullName.ShouldBe(createUserCommand.UserFullName);

            userDto.UserNickName.ShouldBe(createUserCommand.UserNickName);

            userDto.UserEmailAddress.ShouldBe(createUserCommand.UserEmailAddress);
        }
    }
}
