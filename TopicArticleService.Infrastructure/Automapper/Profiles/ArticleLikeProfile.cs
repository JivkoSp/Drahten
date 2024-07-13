using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class ArticleLikeProfile : Profile
    {
        public ArticleLikeProfile()
        {
            CreateMap<ArticleLikeReadModel, ArticleLikeDto>();
        }
    }
}
