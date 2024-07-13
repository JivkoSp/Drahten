using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class ArticleCommentProfile : Profile
    {
        public ArticleCommentProfile()
        {
            CreateMap<ArticleCommentReadModel, ArticleCommentDto>()
                .ForMember(dest => dest.CommentValue, options => options.MapFrom(source => source.Comment))
                .ForMember(dest => dest.ArticleDto, options => options.MapFrom(source => source.Article))
                .ForMember(dest => dest.ArticleCommentLikeDtos, options => options.MapFrom(source => source.ArticleCommentLikes))
                .ForMember(dest => dest.ArticleCommentDislikeDtos, options => options.MapFrom(source => source.ArticleCommentDislikes));
        }
    }
}
