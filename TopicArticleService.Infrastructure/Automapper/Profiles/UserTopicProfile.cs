using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class UserTopicProfile : Profile
    {
        public UserTopicProfile()
        {
            CreateMap<UserTopicReadModel, UserTopicDto>()
                .ForMember(dest => dest.TopicName, options => options.MapFrom(source => source.Topic.TopicName))
                .ForMember(dest => dest.TopicFullName, options => options.MapFrom(source => source.Topic.TopicFullName));
        }
    }
}
