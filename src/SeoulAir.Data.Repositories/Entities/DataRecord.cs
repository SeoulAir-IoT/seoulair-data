using SeoulAir.Data.Repositories.Attributes;
using System;

namespace SeoulAir.Data.Repositories.Entities
{
    [BsonCollection("dataRecords")]
    public class DataRecord : BaseEntityWithId
    {
        public DateTime MeasurementDate { get; set; }

        public StationInfo StationInfo { get; set; }

        public AirPollutionInfo AirPollutionInfo { get; set; }
    }
}
