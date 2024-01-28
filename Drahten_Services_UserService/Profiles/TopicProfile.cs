using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<Topic, ReadTopicDto>()
                .ForMember(dest => dest.Children, options => options.MapFrom(source => source.Children));

            
        }
    }
}
