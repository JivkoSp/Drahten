using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal class TopicProfile : Profile
    {
        public TopicProfile()
        {
            CreateMap<TopicReadModel, TopicDto>();
        }
    }
}
