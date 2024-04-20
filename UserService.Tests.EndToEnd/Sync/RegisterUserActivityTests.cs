using Newtonsoft.Json;
using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class RegisterUserActivityTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddToAuditTrailCommand> PrepareAddToAuditTrailCommandAsync()
        {
            var userId = Guid.NewGuid();

            var createUserCommand = new CreateUserCommand(userId, "John Doe", "Johny", "john@mail.com");

            await Post(createUserCommand, "/user-service/users");

            var addToAuditTrailCommand = new AddToAuditTrailCommand(userId, "SignedIn", DateTimeOffset.Now, "https://example.com");

            return addToAuditTrailCommand;
        }

        public RegisterUserActivityTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if UserTracking value object is created from the specified parameters in AddToAuditTrailCommand.
        [Fact]
        public async Task Register_UserActivity_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var addToAuditTrailCommand = await PrepareAddToAuditTrailCommandAsync();

            //ACT
            var response = await Post(addToAuditTrailCommand, $"/user-service/users/{addToAuditTrailCommand.UserId}/user-tracking");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe($"/user-service/users/{addToAuditTrailCommand.UserId}/user-tracking");
        }
    }
}
