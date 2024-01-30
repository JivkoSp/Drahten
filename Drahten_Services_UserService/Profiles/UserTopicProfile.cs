using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class UserTopicProfile : Profile
    {
        public UserTopicProfile()
        {
            CreateMap<WriteUserDto, UserTopic>();
        }
    }
}
