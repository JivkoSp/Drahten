using AutoMapper;
using PrivateHistoryService.Application.Dtos;
using PrivateHistoryService.Infrastructure.EntityFramework.Models;

namespace PrivateHistoryService.Infrastructure.Automapper.Profiles
{
    internal sealed class ViewedUserProfile : Profile
    {
        public ViewedUserProfile()
        {
            CreateMap<ViewedUserReadModel, ViewedUserDto>()
                .ForMember(dest => dest.RetentionUntil, options => options.MapFrom(source => source.ViewedUser.RetentionUntil));
        }
    }
}
