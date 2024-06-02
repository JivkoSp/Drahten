using Newtonsoft.Json;
using PrivateHistoryService.Application.Commands;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Presentation.Dtos;
using PrivateHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PrivateHistoryService.Tests.EndToEnd.Sync
{
    public class RemoveViewedUserTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddViewedUserCommand> PrepareAddViewedUserCommandAsync()
        {
            var viewerUserId = Guid.NewGuid();

            var viewedUserId = Guid.NewGuid();

            var command = new AddUserCommand(viewerUserId);

            await Post(command, $"/privatehistory-service/users/{viewerUserId}");

            command = new AddUserCommand(viewedUserId);

            await Post(command, $"/privatehistory-service/users/{viewedUserId}");

            var addViewedUserCommand = new AddViewedUserCommand(ViewerUserId: viewerUserId, ViewedUserId: viewedUserId,
                DateTime: DateTimeOffset.Now);

            return addViewedUserCommand;
        }

        private async Task<ViewedUserDto> GetViewedUserDtoAsync(Guid userId)
        {
            var viewedUsersResponse = await Get($"/privatehistory-service/users/{userId}/viewed-users/");

            var responseSerializedContent = await viewedUsersResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            var responseResult = JsonConvert.DeserializeObject<List<ViewedUserDto>>(Convert.ToString(responseDto.Result));

            return responseResult.FirstOrDefault();
        }

        private async Task<RemoveViewedUserCommand> PrepareRemoveViewedUserCommandAsync()
        {
            var command = await PrepareAddViewedUserCommandAsync();

            await Post(command, $"/privatehistory-service/users/{command.ViewerUserId}/viewed-users/{command.ViewedUserId}");

            var viewedUserDto = await GetViewedUserDtoAsync(command.ViewerUserId);

            var removeViewedUserCommand = new RemoveViewedUserCommand(ViewerUserId: command.ViewerUserId,
                ViewedUserId: viewedUserDto.ViewedUserReadModelId);

            return removeViewedUserCommand;
        }

        public RemoveViewedUserTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 204 if ViewedUser value object that has Id like the ViewedUserId,
        //specified in RemoveViewedUserCommand is deleted from the database.
        [Fact]
        public async Task Remove_Viewed_User_Endpoint_Should_Return_Http_Status_Code_NoContent()
        {
            //ARRANGE
            var command = await PrepareRemoveViewedUserCommandAsync();

            //ACT
            var response = await Delete($"/privatehistory-service/users/{command.ViewerUserId}/viewed-users/{command.ViewedUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("No Content");

            response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        //Should return http status code 404 when the previously deleted ViewedUser value object is not found in the database.
        [Fact]
        public async Task Remove_Viewed_User_Endpoint_Should_Remove_ViewedUser_From_The_Database()
        {
            //ARRANGE
            var command = await PrepareRemoveViewedUserCommandAsync();

            await Delete($"/privatehistory-service/users/{command.ViewerUserId}/viewed-users/{command.ViewedUserId}");

            //ACT
            var response = await Get($"/privatehistory-service/users/{command.ViewerUserId}/viewed-users/");

            //ASSERT
            response.ShouldNotBeNull();

            response.ReasonPhrase.ShouldBe("Not Found");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
