using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class PublicHistoryProfile : Profile
    {
        public PublicHistoryProfile()
        {
            CreateMap<WritePublicHistoryDto, PublicHistory>()
                .ForMember(dest => dest.UserId, options => options.MapFrom(source => source.PublicHistoryId));

            CreateMap<PublicHistory, ReadPublicHistoryDto>()
                .ForMember(dest => dest.PublicHistoryId, options => options.MapFrom(source => source.UserId));
        }
    }
}
