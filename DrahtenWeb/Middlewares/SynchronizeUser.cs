using DrahtenWeb.Dtos;
using DrahtenWeb.Exceptions;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
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

                if (response != null && response.IsSuccess == false)
                {
                    //The user is NOT registered in UserService.
                    //Register the user (synchronize the user in UserService).

                    await userService.RegisterUser<ResponseDto>(new WriteUserDto { UserId = userId }, accessToken);
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
