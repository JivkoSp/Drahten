using DrahtenWeb.Dtos;
using DrahtenWeb.Exceptions;
using DrahtenWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
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
                var response = new ResponseDto();
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

                response = await userService.GetUserById<ResponseDto>(userId, accessToken);

                var userDto = JsonConvert.DeserializeObject<ReadUserDto>(Convert.ToString(response.Result));

                if (userDto == null)
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

                response = await userService.GetUserPrivateHistory<ResponseDto>(userId, accessToken);

                var userPrivateHistoryDto = JsonConvert.DeserializeObject<ReadPrivateHistoryDto>(Convert.ToString(response.Result));

                if(userPrivateHistoryDto == null) 
                {
                    var writeUserPrivateHistoryDto = new WritePrivateHistoryDto
                    {
                        PrivateHistoryId = userId,
                        HistoryLiveTime = DateTime.Now.AddHours(24),
                    };

                    await userService.CreateUserPrivateHistory<ResponseDto>(writeUserPrivateHistoryDto, accessToken);
                }

                response = await userService.GetUserPublicHistory<ResponseDto>(userId, accessToken);

                var userPublicHistoryDto = JsonConvert.DeserializeObject<ReadPublicHistoryDto>(Convert.ToString(response.Result));

                if(userPublicHistoryDto == null)
                {
                    var writeUserPublicHistoryDto = new WritePublicHistoryDto
                    {
                        PublicHistoryId = userId
                    };

                    await userService.CreateUserPublicHistory<ResponseDto>(writeUserPublicHistoryDto, accessToken);
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
