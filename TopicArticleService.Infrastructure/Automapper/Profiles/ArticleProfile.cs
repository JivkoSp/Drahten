using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleReadModel, ArticleDto>()
                .ForMember(dest => dest.ArticleLikeDtos, options => options.MapFrom(source => source.ArticleLikes))
                .ForMember(dest => dest.ArticleDislikeDtos, options => options.MapFrom(source => source.ArticleDislikes))
                .ForMember(dest => dest.TopicFullName, options => options.MapFrom(source => source.Topic.TopicFullName));
        }
    }
}
