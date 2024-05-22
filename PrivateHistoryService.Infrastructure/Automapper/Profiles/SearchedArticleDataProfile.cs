using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class SearchedArticleDataProfile : Profile
    {
        public SearchedArticleDataProfile()
        {
            CreateMap<SearchedArticleDataReadModel, SearchedArticleDataDto>();
        }
    }
}
