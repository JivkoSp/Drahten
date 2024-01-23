using DrahtenWeb.Dtos;
using DrahtenWeb.Models;

namespace DrahtenWeb.Services.IServices
{
    public interface IBaseService
    {
        ResponseDto Response { get; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
