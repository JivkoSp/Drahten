using Newtonsoft.Json;
using PublicHistoryService.Application.Commands;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Presentation.Dtos;
using PublicHistoryService.Tests.EndToEnd.Factories;
using Shouldly;
using System.Net;
using Xunit;

namespace PublicHistoryService.Tests.EndToEnd.Sync
{
    public class AddViewedUserTests : BaseSyncIntegrationTest
    {
        #region GLOBAL ARRANGE

        private async Task<AddViewedUserCommand> PrepareAddViewedUserCommandAsync()
        {
            var viewerUserId = Guid.NewGuid();

            var viewedUserId = Guid.NewGuid();

            var command = new AddUserCommand(viewerUserId);

            await Post(command, $"/publichistory-service/users/{viewerUserId}");

            command = new AddUserCommand(viewedUserId);

            await Post(command, $"/publichistory-service/users/{viewedUserId}");

            var addViewedUserCommand = new AddViewedUserCommand(ViewerUserId: viewerUserId, ViewedUserId: viewedUserId,
                DateTime: DateTimeOffset.Now);

            return addViewedUserCommand;
        }

        public AddViewedUserTests(PrivateHistoryServiceApplicationFactory factory) : base(factory)
        {
        }

        #endregion

        //Should return http status code 201 if ViewedUser value object is created from the specified parameters in AddViewedUserCommand.
        [Fact]
        public async Task Add_Viewed_User_Endpoint_Should_Return_Http_Status_Code_Created()
        {
            //ARRANGE
            var command = await PrepareAddViewedUserCommandAsync();

            //ACT
            var response = await Post(command,
                $"/publichistory-service/users/{command.ViewerUserId}/viewed-users/{command.ViewedUserId}");

            //ASSERT
            response.ShouldNotBeNull();

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            response.Headers.Location.ShouldNotBeNull();

            response.Headers.Location.ToString()
                .ShouldBe($"/publichistory-service/users/{command.ViewerUserId}/viewed-users/{command.ViewedUserId}");
        }

        //Should return http status code 200 when the previously created ViewedUser value object is successfully received from the database.
        [Fact]
        public async Task Add_Viewed_User_Endpoint_Should_Add_ViewedUser_To_The_Database()
        {
            //ARRANGE
            var command = await PrepareAddViewedUserCommandAsync();

            //ACT
            await Post(command, $"/publichistory-service/users/{command.ViewerUserId}/viewed-users/{command.ViewedUserId}");

            var viewedUsersResponse = await Get($"/publichistory-service/users/{command.ViewerUserId}/viewed-users/");

            //ASSERT
            viewedUsersResponse.ShouldNotBeNull();

            viewedUsersResponse.StatusCode.ShouldBe(HttpStatusCode.OK);

            var responseSerializedContent = await viewedUsersResponse.Content.ReadAsStringAsync();

            var responseDto = JsonConvert.DeserializeObject<ResponseDto>(responseSerializedContent);

            responseDto.ShouldNotBeNull();

            responseDto.IsSuccess.ShouldBeTrue();

            var responseResult = JsonConvert.DeserializeObject<List<ViewedUserDto>>(Convert.ToString(responseDto.Result));

            responseResult.ShouldNotBeNull();

            var viewedUserDto = responseResult.FirstOrDefault().ShouldNotBeNull();

            viewedUserDto.ViewerUserId.ShouldBe(command.ViewerUserId.ToString());

            viewedUserDto.ViewedUserId.ShouldBe(command.ViewedUserId.ToString());

            //*** IMPORTANT - Time comparison ***
            //See the _README.txt in this directory.

            TimeSpan tolerance = TimeSpan.FromMilliseconds(1); // Tolerance of 1 millisecond

            bool withinTolerance = Math.Abs((command.DateTime - viewedUserDto.DateTime).TotalMilliseconds)
                <= tolerance.TotalMilliseconds;

            withinTolerance.ShouldBeTrue();
        }
    }
}
