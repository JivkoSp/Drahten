using PublicHistoryService.Application.Commands;
using PublicHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PublicHistoryService.Tests.EndToEnd.Sync
{
    public class RegisterUserTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        public RegisterUserTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if User domain entity is created from the specified parameters in AddUserCommand.
        [Fact]
        public async Task Register_User_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var userId = Guid.NewGuid();

            var command = new AddUserCommand(userId);

            //ACT
            var response = await Post(command, $"/publichistory-service/users/{command.UserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/publichistory-service/users/{command.UserId}");
        }
    }
}
