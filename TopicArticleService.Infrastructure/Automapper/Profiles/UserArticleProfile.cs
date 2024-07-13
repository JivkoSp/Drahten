using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    internal sealed class UserArticleProfile : Profile
    {
        public UserArticleProfile()
        {
            CreateMap<UserArticleReadModel, UserArticleDto>()
                .ForMember(dest => dest.UserDto, options => options.MapFrom(source => source.User))
                .ForMember(dest => dest.ArticleDto, options => options.MapFrom(source => source.Article));

            CreateMap<UserArticleReadModel, UserDto>();
        }
    }
}
