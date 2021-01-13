using AutoMapper;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Repositories.Entities;

namespace SeoulAir.Data.Api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<DataRecord, DataRecordDto>().ReverseMap();
            CreateMap<StationInfoDto, StationInfo>().ReverseMap();
            CreateMap<AirPollutionInfoDto, AirPollutionInfo>().ReverseMap();
        }
    }
}
