using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class ArticleLikeProfile : Profile
    {
        public ArticleLikeProfile()
        {
            CreateMap<ArticleLike, ReadArticleLikeDto>();
        }
    }
}
