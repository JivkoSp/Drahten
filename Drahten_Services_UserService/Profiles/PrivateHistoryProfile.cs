using AutoMapper;
using Drahten_Services_UserService.Dtos;
using Drahten_Services_UserService.Models;

namespace Drahten_Services_UserService.Profiles
{
    public class PrivateHistoryProfile : Profile
    {
        public PrivateHistoryProfile()
        {
            CreateMap<WritePrivateHistoryDto, PrivateHistory>()
                .ForMember(dest => dest.UserId, options => options.MapFrom(source => source.PrivateHistoryId));

            CreateMap<PrivateHistory, ReadPrivateHistoryDto>()
                .ForMember(dest => dest.PrivateHistoryId, options => options.MapFrom(source => source.UserId));
        }
    }
}
