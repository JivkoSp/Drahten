using AutoMapper;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class SearchedArticleDataProfile : Profile
    {
        public SearchedArticleDataProfile()
        {
            CreateMap<SearchedArticleDataReadModel, SearchedArticleDataDto>();

            CreateMap<SearchedArticleDataDto, SearchedArticleData>()
               .ConstructUsing(source =>
                   new SearchedArticleData(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.SearchedData, source.DateTime.ToUniversalTime()));
        }
    }
}
