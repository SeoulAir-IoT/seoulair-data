namespace SeoulAir.Data.Domain.Resources
{
    public static class Strings
    {
        public const string InvalidConfigurationAttribute = "Invalid configuration file.\nAttribute: '{0}' is not set";
        public const string FileDoesNotExistMessage = "Specified file does not exist or does not match required extension.";
        public const string InvalidFileFormatMessage = "Specified file does not match format that is required for the device.";
        public const string InvalidDateFormatMessage = "Application does not support provided date format.";
        public const string InvalidStationCodeMessage = "Application does not support provided station code format";
        public const string InvalidColumnTypeMessage = "Column {0} has unexpected data type.";

        public const string InternalServerErrorUri = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        public const string NotImplementedUri = "https://tools.ietf.org/html/rfc7231#section-6.6.2";

        public const string InternalServerErrorTitle = "500 Internal Server Error";
        public const string NotImplementedTitle = "501 Not Implemented";
    }
}
