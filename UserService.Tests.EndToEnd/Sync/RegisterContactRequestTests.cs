using Newtonsoft.Json;
using Shouldly;
using System.Net;
using UserService.Application.Commands;
using UserService.Application.Dtos;
using UserService.Tests.EndToEnd.Factories;
using Xunit;

namespace UserService.Tests.EndToEnd.Sync
{
    public class RegisterContactRequestTests : BaseSyncIntegrationTest
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

            var addContactRequestCommand = new AddContactRequestCommand(issuerUserId, receiverUserId, DateTimeOffset.Now);

            return addContactRequestCommand;
        }

        public RegisterContactRequestTests(UserServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if ContactRequest is created from the specified parameters in AddContactRequestCommand.
        [Fact]
        public async Task Register_ContactRequest_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            //ACT
            var response = await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");
        }

        //Should return http status code 200 when the previously created ContactRequest is successfully received from the database.
        //*** IMPORTANT - The previously created ContactRequest should be available as a issued ContactRequest for the ISSUER.
        [Fact]
        public async Task Register_ContactRequest_Endpoint_Should_Add_ContactRequest_With_Given_IssuerUserId_And_ReceiverUserId_To_The_Database_For_Issuer()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            //ACT
            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            var response = await Get($"/user-service/users/{addContactRequestCommand.IssuerUserId}/issued-contact-requests-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var issuedContactRequestByUserDto = JsonConvert.DeserializeObject<List<IssuedContactRequestByUserDto>>(responseSerializedContent)
                ?.FirstOrDefault();

            issuedContactRequestByUserDto.ShouldNotBeNull();

            issuedContactRequestByUserDto.IssuerDto.UserId.ShouldBe(addContactRequestCommand.IssuerUserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((addContactRequestCommand.DateTime - issuedContactRequestByUserDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }

        //Should return http status code 200 when the previously created ContactRequest is successfully received from the database.
        //*** IMPORTANT - The previously created ContactRequest should be available as a received ContactRequest for the RECEIVER.
        [Fact]
        public async Task Register_ContactRequest_Endpoint_Should_Add_ContactRequest_With_Given_IssuerUserId_And_ReceiverUserId_To_The_Database_For_Receiver()
        {
            //ARRANGE
            var addContactRequestCommand = await PrepareAddContactRequestCommandAsync();

            //ACT
            await Post(addContactRequestCommand,
                $"/user-service/users/{addContactRequestCommand.IssuerUserId}/contact-requests/{addContactRequestCommand.ReceiverUserId}");

            var response = await Get($"/user-service/users/{addContactRequestCommand.ReceiverUserId}/received-contact-requests-by-user/");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await response.Content.ReadAsStringAsync();

            var receivedContactRequestByUserDto = JsonConvert.DeserializeObject<List<ReceivedContactRequestByUserDto>>(responseSerializedContent)
                ?.FirstOrDefault();

            receivedContactRequestByUserDto.ShouldNotBeNull();

            receivedContactRequestByUserDto.ReceiverDto.UserId.ShouldBe(addContactRequestCommand.ReceiverUserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((addContactRequestCommand.DateTime - receivedContactRequestByUserDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
