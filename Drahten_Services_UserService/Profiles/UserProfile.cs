using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ReadUserDto>();
            CreateMap<WriteUserDto, User>();
        }
    }
}
