using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Application.Extensions;
using PrivateHistoryService.Domain.ValueObjects;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class SearchedArticleDataProfile : Profile
    {
        public SearchedArticleDataProfile()
        {
            CreateMap<SearchedArticleDataReadModel, SearchedArticleDataDto>()
                .ForMember(dest => dest.RetentionUntil, options => options.MapFrom(source => source.User.RetentionUntil));

            CreateMap<SearchedArticleDataDto, SearchedArticleData>()
               .ConstructUsing(source =>
                   new SearchedArticleData(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.SearchedData, 
                   source.SearchedDataAnswer, source.SearchedDataAnswerContext, source.DateTime.ToUtc()));
        }
    }
}
