using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal class ArticleDislikeProfile : Profile
    {
        public ArticleDislikeProfile()
        {
            CreateMap<ArticleDislikeReadModel, ArticleDislikeDto>();
        }
    }
}
