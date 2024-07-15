using AutoMapper;
using UserService.Application.Dtos;
using UserService.Infrastructure.EntityFramework.Models;

namespace UserService.Infrastructure.Automapper.Profiles
{
    internal sealed class ContactRequestProfile : Profile
    {
        public ContactRequestProfile()
        {
            CreateMap<ContactRequestReadModel, IssuedContactRequestByUserDto>()
                .ForMember(dest => dest.IssuerDto, options => options.MapFrom(source => source.Issuer));

            CreateMap<ContactRequestReadModel, ReceivedContactRequestByUserDto>()
                .ForMember(dest => dest.ReceiverDto, options => options.MapFrom(source => source.Receiver));
        }
    }
}
