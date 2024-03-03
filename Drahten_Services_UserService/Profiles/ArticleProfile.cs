using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<WriteArticleDto, Article>();

            CreateMap<Article, ReadArticleDto>();
        }
    }
}
