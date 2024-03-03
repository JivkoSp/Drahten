using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class ArticleCommentProfile : Profile
    {
        public ArticleCommentProfile()
        {
            CreateMap<ArticleComment, ReadArticleCommentDto>()
                .ForMember(dest => dest.UserDto, options => options.MapFrom(source => source.User));

            CreateMap<WriteArticleCommentDto, ArticleComment>();
        }
    }
}
