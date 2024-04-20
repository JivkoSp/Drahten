
using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class RemoveReceivedContactRequestTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddContactRequestCommand> PrepareAddContactRequestCommandAsync()
        {
            var issuerUserId = Guid.NewGuid();

            var receiverUserId = Guid.NewGuid();

            var createUserCommand = new CreateUserCommand(issuerUserId, "John Doe", "Johny", "john@mail.com");

            await Post(createUserCommand, "/user-service/users");

            createUserCommand = new CreateUserCommand(receiverUserId, "John Doe", "Johny", "john@mail.com");

            await Post(createUserCommand, "/user-service/users");

            var addContactRequestCommand = new AddContactRequestCommand(issuerUserId, receiverUserId, DateTimeOffset.Now, "original message");

            return addContactRequestCommand;
        }

        public RemoveReceivedContactRequestTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if ContactRequest that has parameters like the ones that are
        //specified in AddContactRequestCommand is deleted from the database.
        [Fact]
        public async Task Remove_Received_ContactRequest_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            //ACT
            var response = await Delete($"/user-service/users/{addContactRequestCommand.ReceiverUserId}/received-contact-requests/{addContactRequestCommand.IssuerUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted ContactRequest is not found in the database for the RECEIVER.
        [Fact]
        public async Task Remove_Received_ContactRequest_Endpoint_Should_Remove_ContactRequest_From_The_Database_For_Receiver()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            await Delete($"/user-service/users/{addContactRequestCommand.ReceiverUserId}/received-contact-requests/{addContactRequestCommand.IssuerUserId}");

            //ACT
            var response = await Get($"/user-service/users/{addContactRequestCommand.ReceiverUserId}/received-contact-requests-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

        //Should return http status code 404 when the previously deleted ContactRequest is not found in the database for the ISSUER.
        [Fact]
        public async Task Remove_Received_ContactRequest_Endpoint_Should_Remove_ContactRequest_From_The_Database_For_Issuer()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            await Delete($"/user-service/users/{addContactRequestCommand.ReceiverUserId}/received-contact-requests/{addContactRequestCommand.IssuerUserId}");

            //ACT
            var response = await Get($"/user-service/users/{addContactRequestCommand.IssuerUserId}/issued-contact-requests-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
