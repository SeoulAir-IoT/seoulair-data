
namespace SeoulAir.Data.Domain.Dtos
{
    public class StationInfoDto
    {
        public ushort StationCode { get; set; }
        public string StationAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
