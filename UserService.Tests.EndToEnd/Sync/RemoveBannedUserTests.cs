using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class RemoveBannedUserTests : BaseSyncIntegrationTest
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

        public RemoveBannedUserTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if BannedUser value object that has parameters like the ones that are
        //specified in the BanUserCommand is deleted from the database.
        [Fact]
        public async Task Remove_BannedUser_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var banUserCommand = await PrepareBanUserCommandAsync();

            await Post(banUserCommand,
               $"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");

            //ACT
            var response = await Delete($"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted BannedUser is not found in the database.
        [Fact]
        public async Task Remove_BannedUser_Endpoint_Should_Remove_BannedUser_From_The_Database()
        {
            //ARRANGE
            var banUserCommand = await PrepareBanUserCommandAsync();

            await Post(banUserCommand,
               $"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");

            await Delete($"/user-service/users/{banUserCommand.IssuerUserId}/banned-users/{banUserCommand.ReceiverUserId}");

            //ACT
            var response = await Get($"/user-service/users/{banUserCommand.IssuerUserId}/issued-bans-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
