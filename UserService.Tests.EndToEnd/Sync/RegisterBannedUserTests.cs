using Newtonsoft.Json;
using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Presentation.Dtos;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class RegisterBannedUserTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<BanUserCommand> PrepareBanUserCommandAsync()
        {
            var issuerUserId = Guid.NewGuid();  

            var receiverUserId = Guid.NewGuid();

            var createUserCommand = new CreateUserCommand(issuerUserId, "John Doe", "Johny", "john@mail.com");

            await Post(createUserCommand, "/user-service/users");

            createUserCommand = new CreateUserCommand(receiverUserId, "John Doe", "Johny", "john@mail.com");

            await Post(createUserCommand, "/user-service/users");

            var banUserCommand = new BanUserCommand(issuerUserId, receiverUserId, DateTimeOffset.Now);

            return banUserCommand;
        }

        public RegisterBannedUserTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if BannedUser is created from the specified parameters in BanUserCommand.
        [Fact]
        public async Task Register_BannedUser_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var banUserCommand = await PrepareBanUserCommandAsync();

            //ACT
            var response = await Post(banUserCommand, 
                $"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");
        }

        //Should return http status code 200 when the previously created BannedUser is successfully received from the database.
        [Fact]
        public async Task Register_BannedUser_Endpoint_Should_Add_BannedUser_With_Given_IssuerUserId_And_ReceiverUserId_To_The_Database()
        {
            //ARRANGE
            var banUserCommand = await PrepareBanUserCommandAsync();

            //ACT
            await Post(banUserCommand, $"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");

            var issuedUserBansResponse = await Get($"/user-service/users/{banUserCommand.IssuerUserId}/issued-bans-by-user/");

            //ASSERT
            issuedUserBansResponse.ShouldNotBeNull();

            issuedUserBansResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await issuedUserBansResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<IssuedBanByUserDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var issuedBanByUserDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            issuedBanByUserDto.IssuerDto.UserId.ShouldBe(banUserCommand.IssuerUserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((banUserCommand.DateTime - issuedBanByUserDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
