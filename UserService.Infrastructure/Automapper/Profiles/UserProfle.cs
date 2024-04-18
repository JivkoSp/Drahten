using AutoMapper;
using UserService.Application.Dtos;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.Automapper.Profiles
{
    internal class UserProfle : Profile
    {
        public UserProfle()
        {
            CreateMap<UserReadModel, UserDto>();
        }
    }
}
