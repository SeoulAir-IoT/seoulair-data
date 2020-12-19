namespace SeoulAir.Data.Domain.Dtos
{
    public class AppSettings
    {
        public MongoDbSettings mongoDbSettings { get; set; }
        public MqttSettings mqttSettings { get; set; }
    }
}
