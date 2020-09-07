using AutoMapper;
using MongoDB.Bson;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Repositories.Entities;

namespace SeoulAir.Data.Api.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullDestinationValues = true;
            CreateMap<DataRecord, DataRecordDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<DataRecordDto, DataRecord>()
                .ForMember(entity => entity.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

            CreateMap<StationInfoDto, StationInfo>().ReverseMap();

            CreateMap<AirPollutionInfoDto, AirPollutionInfo>().ReverseMap();
        }
    }
}
