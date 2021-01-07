namespace SeoulAir.Data.Domain.Options
{
    public class MongoDbOptions
    {
        public static string AppSettingsPath { get; } = "MongoDbSettings";
        
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}