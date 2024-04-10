using Shouldly;
using System.Net;
using TopicArticleService.Application.Commands;
using TopicArticleService.Tests.EndToEnd.Factories;
using Xunit;

namespace TopicArticleService.Tests.EndToEnd.Sync
{
    public class RegisterUserTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private RegisterUserCommand PrepareRegisterUserCommandAsync()
        {
            var registerUserCommand = new RegisterUserCommand(Guid.NewGuid());

            return registerUserCommand;
        }

        public RegisterUserTests(DrahtenApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if User is created from the specified parameters in RegisterUserCommand.
        [Fact]
        public async Task Register_User_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var registerUserCommand = PrepareRegisterUserCommandAsync();

            //ACT
            var response = await Post(registerUserCommand, "/topic-article-service/users");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString().ShouldBe("/topic-article-service/users");
        }
    }
}
