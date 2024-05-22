using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class ViewedArticleProfile : Profile
    {
        public ViewedArticleProfile()
        {
            CreateMap<ViewedArticleReadModel, ViewedArticleDto>();
        }
    }
}
