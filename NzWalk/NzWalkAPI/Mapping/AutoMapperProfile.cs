using AutoMapper;
using NzWalkAPI.Models.Domain;
using NzWalkAPI.Models.DTO;

namespace NzWalkAPI.Mapping
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionDto, Region>().ReverseMap();
            CreateMap<UpdateRegionDto, Region>().ReverseMap();
        }
    }
}
