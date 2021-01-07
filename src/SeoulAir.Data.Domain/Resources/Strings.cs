namespace SeoulAir.Data.Domain.Resources
{
    public static class Strings
    {
        public const string InvalidConfigurationAttribute = "Invalid configuration file.\nAttribute: '{0}' is not set";
        public const string InvalidStationCodeMessage = "Application does not support provided station code format";
        public const string MqttConnectingExceptionMessage = "Error ocured while trying to connect to mqtt broker.";
        public const string ReadingMessageExceptionMessage = "Error ocured while trying to read the message.";

        public const string InternalServerErrorUri = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        public const string NotImplementedUri = "https://tools.ietf.org/html/rfc7231#section-6.6.2";

        public const string InternalServerErrorTitle = "500 Internal Server Error";
        public const string NotImplementedTitle = "501 Not Implemented";
        
        public const string ParameterNullOrEmptyMessage = "Parameter {0} must not be null or empty string.";
        public const string ParameterBetweenMessage = "Value of parameter {0} must be between {1} and {2}.";
    }
}
