using DrahtenWeb.Dtos;
using DrahtenWeb.Dtos.UserService;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Security.Claims;

namespace DrahtenWeb.Middlewares
{
    public class SynchronizeUser
    {
        private readonly RequestDelegate _requestDelegate;

        public SynchronizeUser(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                var userDto = new UserDto();
                var userId = Guid.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var accessToken = await context.GetTokenAsync("access_token");

                var response = await userService.GetUserByIdAsync<ResponseDto>(userId, accessToken);

                if(response != null && response.IsSuccess)
                {
                    userDto = JsonConvert.DeserializeObject<UserDto>(Convert.ToString(response.Result));
                }
                else
                {
                    userDto = null;
                }
                
                if (userDto == null)
                {
                    userDto = new UserDto
                    {
                        UserId = userId,
                        UserFullName = context.User.FindFirstValue("name"),
                        UserNickName = context.User.FindFirstValue("preferred_username"),
                        UserEmailAddress = context.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
                    };

                    await userService.RegisterUserAsync<ResponseDto>(userDto, accessToken);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await _requestDelegate(context);
        }
    }
}
