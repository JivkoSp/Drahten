using DrahtenWeb.Dtos;
using DrahtenWeb.Exceptions;
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
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var accessToken = await context.GetTokenAsync("access_token");

                if (userId == null)
                {
                    throw new ClaimsPrincipalNameIdentifierNotFoundException();
                }

                if(accessToken == null)
                {
                    throw new HttpContextAccessTokenNotFoundException();
                }

                var response = await userService.GetUserById<ResponseDto>(userId, accessToken);

                var userModel = JsonConvert.DeserializeObject<ReadUserDto>(Convert.ToString(response.Result));

                if (userModel == null)
                {
                    //The user is NOT registered in UserService.
                    //Register the user (synchronize the user in UserService).

                    var writeUserDto = new WriteUserDto
                    {
                        UserId = userId,
                        FullName = context.User.FindFirstValue("name") ?? "",
                        NickName = context.User.FindFirstValue("preferred_username") ?? "",
                        EmailAddress = context.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") ?? ""
                    };

                    await userService.RegisterUser<ResponseDto>(writeUserDto, accessToken);
                }
            }
            catch(ClaimsPrincipalNameIdentifierNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(HttpContextAccessTokenNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            await _requestDelegate(context);
        }
    }
}
