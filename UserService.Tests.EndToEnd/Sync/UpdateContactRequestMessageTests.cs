using Newtonsoft.Json;
using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class UpdateContactRequestMessageTests : BaseSyncIntegrationTest
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

        public UpdateContactRequestMessageTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if a ContactRequest message is successfully updated,
        //using the specified parameters in UpdateContactRequestMessageCommand.
        [Fact]
        public async Task Update_ContactRequestMessage_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            var updateContactRequestMessageCommand = new UpdateContactRequestMessageCommand(addContactRequestCommand.IssuerUserId, 
                addContactRequestCommand.ReceiverUserId, "new message", DateTimeOffset.Now);

            //ACT
            var response = await Put(updateContactRequestMessageCommand, 
                $"/user-service/users/{updateContactRequestMessageCommand.IssuerUserId}/update-contact-request-message/{updateContactRequestMessageCommand.ReceiverUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 200 when the previously updated ContactRequest message is successfully received from the database.
        //*** IMPORTANT - The previously updated ContactRequest message should be different than the ContactRequest message before the update.
        //The ContactRequest should be available as a issued ContactRequest from the ISSUER.
        [Fact]
        public async Task Update_ContactRequestMessage_Endpoint_Should_Update_ContactRequestMessage_With_Given_IssuerUserId_And_ReceiverUserId_From_The_Database_For_Issuer()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            var updateContactRequestMessageCommand = new UpdateContactRequestMessageCommand(addContactRequestCommand.IssuerUserId,
                addContactRequestCommand.ReceiverUserId, "new message", DateTimeOffset.Now);

            //ACT
            await Put(updateContactRequestMessageCommand,
               $"/user-service/users/{updateContactRequestMessageCommand.IssuerUserId}/update-contact-request-message/{updateContactRequestMessageCommand.ReceiverUserId}");

            var response = await Get($"/user-service/users/{addContactRequestCommand.IssuerUserId}/issued-contact-requests-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var issuedContactRequestByUserDto = JsonConvert.DeserializeObject<List<IssuedContactRequestByUserDto>>(responseSerializedContent)
                ?.FirstOrDefault();

            issuedContactRequestByUserDto.ShouldNotBeNull();

            issuedContactRequestByUserDto.IssuerDto.UserId.ShouldBe(updateContactRequestMessageCommand.IssuerUserId.ToString());

            issuedContactRequestByUserDto.Message.ShouldBe(updateContactRequestMessageCommand.Message);

            addContactRequestCommand.Message.ShouldNotBeSameAs(updateContactRequestMessageCommand.Message);
        }

        //Should return http status code 200 when the previously updated ContactRequest message is successfully received from the database.
        //*** IMPORTANT - The previously updated ContactRequest message should be different than the ContactRequest message before the update.
        //The ContactRequest should be available as a received ContactRequest from the RECEIVER.
        [Fact]
        public async Task Update_ContactRequestMessage_Endpoint_Should_Update_ContactRequestMessage_With_Given_IssuerUserId_And_ReceiverUserId_From_The_Database_For_Receiver()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            var updateContactRequestMessageCommand = new UpdateContactRequestMessageCommand(addContactRequestCommand.IssuerUserId,
                addContactRequestCommand.ReceiverUserId, "new message", DateTimeOffset.Now);

            //ACT
            await Put(updateContactRequestMessageCommand,
               $"/user-service/users/{updateContactRequestMessageCommand.IssuerUserId}/update-contact-request-message/{updateContactRequestMessageCommand.ReceiverUserId}");

            var response = await Get($"/user-service/users/{addContactRequestCommand.ReceiverUserId}/received-contact-requests-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var receivedContactRequestByUserDto = JsonConvert.DeserializeObject<List<ReceivedContactRequestByUserDto>>(responseSerializedContent)
                ?.FirstOrDefault();

            receivedContactRequestByUserDto.ShouldNotBeNull();

            receivedContactRequestByUserDto.ReceiverDto.UserId.ShouldBe(updateContactRequestMessageCommand.ReceiverUserId.ToString());

            receivedContactRequestByUserDto.Message.ShouldBe(updateContactRequestMessageCommand.Message);

            addContactRequestCommand.Message.ShouldNotBeSameAs(updateContactRequestMessageCommand.Message);
        }
    }
}