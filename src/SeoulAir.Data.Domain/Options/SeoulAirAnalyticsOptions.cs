namespace SeoulAir.Data.Domain.Options
{
    public class SeoulAirAnalyticsOptions : MicroserviceUrlOptions
    {
        public static string AppSettingsPath { get; } 
            = "MicroserviceUrlOptions:SeoulAir.Analytics";
    }
}
