namespace SeoulAir.Data.Domain.Options
{
    public class MqttConnectionOptions
    {
        public static string AppSettingsPath { get; } = "Mqtt";
        
        public string BrokerAddress { get; set; }
        public int BrokerPort { get; set; }
        public string Topic { get; set; }
        public string DataReceiverClientId { get; set; }
    }
}