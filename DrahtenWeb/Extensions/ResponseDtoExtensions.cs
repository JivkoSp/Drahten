using DrahtenWeb.Dtos;
using Newtonsoft.Json;

namespace DrahtenWeb.Extensions
{
    public static class ResponseDtoExtensions
    {
        public static T Map<T>(this ResponseDto responseDto) where T : new ()
        {
            var result = new T();

            if (responseDto != null && responseDto.IsSuccess)
            {
                result = JsonConvert.DeserializeObject<T>(Convert.ToString(responseDto.Result));
            }

            return result;
        }
    }
}
