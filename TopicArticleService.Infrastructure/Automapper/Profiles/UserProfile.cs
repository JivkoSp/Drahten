using AutoMapper;
using TopicArticleService.Application.Dtos;
using TopicArticleService.Domain.Entities;
using TopicArticleService.Infrastructure.EntityFramework.Models;

namespace TopicArticleService.Infrastructure.Automapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserReadModel, UserDto>();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, options => options.MapFrom(source => source.UserId));
        }
    }
}
