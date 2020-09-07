namespace SeoulAir.Data.Repositories.Entities
{
    public class StationInfo
    {
        public ushort StationCode { get; set; }
        public string StationAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
