using AutoMapper;
using PlatformService.Dtos;

namespace PlatformService.Profiles
{
    /// <summary>
    /// Used for mapping with the help of AutoMapper
    /// </summary>
    public class PlatformsProfile : Profile
    {
        public PlatformsProfile()
        {
            // Source -> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<PlatformReadDto, PlatformPublishedDto>();
        }
    }
}
