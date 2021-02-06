namespace SeoulAir.Data.Domain.Resources
{
    public static class Strings
    {
        #region Errors and warnings messages
        
        public const string ParameterNullOrEmptyMessage = "Parameter {0} must not be null or empty string.";
        public const string ParameterBetweenMessage = "Value of parameter {0} must be between {1} and {2}.";
        public const string PaginationOrderError = "Pagination error. Invalid \"Order By\" option: {0}";
        public const string PaginationFilterError = "Pagination error. Invalid \"Filter by\" option: {0}";
        public const string MqttClientConnectionWarning = 
            "Warning: Trying to connect to Mqtt Broker but client is already connected!";
        public const string InvalidParameterValueMessage = "Value of parameter {0} has invalid value.";
        public const string RequestBodyGetException = "Http method GET does not support request body.";


        #endregion
        
        #region Exception Middleware Handler

        public const string MqttConnectingExceptionMessage = "Error ocured while trying to connect to mqtt broker.";
        public const string ReadingMessageExceptionMessage = "Error ocured while trying to read the message.";

        public const string InternalServerErrorUri = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
        public const string NotImplementedUri = "https://tools.ietf.org/html/rfc7231#section-6.6.2";
        
        public const string InternalServerErrorTitle = "500 Internal Server Error";
        public const string NotImplementedTitle = "501 Not Implemented";

        #endregion

        #region Swagger Documentation

        public const string OpenApiInfoProjectName = "SeoulAir.Data API";
        public const string OpenApiInfoTitle = "SeoulAir Data microservice.";
        public const string OpenApiInfoProjectVersion = "1.0.0";
        public const string OpenApiInfoDescription
            = "SeoulAir Data is microservice that is part of SeoulAir project.\n" +
              "For more information visit Gitlab Repository";
        public const string SwaggerEndpoint = "/swagger/{0}/swagger.json";
        public const string GitlabContactName = "Gitlab Repository";
        public const string GitlabRepoUri = "http://gitlab.com/seoulair/seoulair-data.git";

        #endregion
    }
}
