using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicReadModel, TopicDto>();
        }
    }
}
