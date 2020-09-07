using System;

namespace SeoulAir.Data.Domain.Dtos
{
    public class DataRecordDto : BaseDtoWithId
    {
        public DateTime MeasurementDate { get; set; }

        public StationInfoDto StationInfo { get; set; }

        public AirPollutionInfoDto AirPollutionInfo {get;set;}
    }
}
