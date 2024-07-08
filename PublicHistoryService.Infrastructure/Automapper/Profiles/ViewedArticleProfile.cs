using AutoMapper;
using PublicHistoryService.Application.Dtos;
using PublicHistoryService.Domain.ValueObjects;
using PublicHistoryService.Infrastructure.EntityFramework.Models;

namespace PublicHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class ViewedArticleProfile : Profile
    {
        public ViewedArticleProfile()
        {
            CreateMap<ViewedArticleReadModel, ViewedArticleDto>();

            CreateMap<ViewedArticleDto, ViewedArticle>()
                .ConstructUsing(source =>
                    new ViewedArticle(Guid.Parse(source.ArticleId), Guid.Parse(source.UserId), source.DateTime.ToUniversalTime()));
        }
    }
}
