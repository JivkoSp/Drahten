using AutoMapper;
using UserService.Application.Dtos;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.Automapper.Profiles
{
    internal class BannedUserProfile : Profile
    {
        public BannedUserProfile()
        {
            CreateMap<BannedUserReadModel, IssuedBanByUserDto>()
                .ForMember(dest => dest.IssuerDto, options => options.MapFrom(source => source.Issuer));

            CreateMap<BannedUserReadModel, ReceivedBanByUserDto>()
                .ForMember(dest => dest.ReceiverDto, options => options.MapFrom(source => source.Receiver));
        }
    }
}
