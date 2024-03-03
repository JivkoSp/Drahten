using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class UserArticleProfile : Profile
    {
        public UserArticleProfile()
        {
            CreateMap<WriteUserArticleDto, UserArticle>();

            CreateMap<UserArticle, ReadUserArticleDto>()
                .ForMember(dest => dest.UserDto, options => options.MapFrom(source => source.User))
                .ForMember(dest => dest.ArticleDto, options => options.MapFrom(source => source.Article));
        }
    }
}
