using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Builders;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using SeoulAir.Data.Domain.Options;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SeoulAir.Data.Domain.Services.Enums;
using SeoulAir.Data.Domain.Services.Extensions;

namespace SeoulAir.Data.Domain.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IMicroserviceHttpRequestBuilder _requestBuilder;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMicroserviceUriBuilder _uriBuilder;
        private readonly SeoulAirAnalyticsOptions _options;
        private readonly ILogger<IAnalyticsService> _logger;

        public AnalyticsService(IMicroserviceHttpRequestBuilder requestBuilder,
            IMicroserviceUriBuilder uriBuilder,
            IOptions<SeoulAirAnalyticsOptions> options,
            IHttpClientFactory clientFactory,
            ILogger<IAnalyticsService> logger)
        {
            _requestBuilder = requestBuilder;
            _uriBuilder = uriBuilder;
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options.Value;
        }

        public async Task SendDataToAnalyticsService<TDto>(TDto dto)
        {
            HttpResponseMessage response;
            DataRecordDto dataRecord = dto as DataRecordDto;

            _uriBuilder.Restart()
                .UseMicroserviceUrlOptions(_options)
                .UseController(AnalyticsControllers.DataRecord.GetDescription())
                .SetEndpoint("process");

            var request = _requestBuilder.Restart()
                .UseHttpMethod(HttpMethod.Post)
                .UseRequestBody(new
                {
                    dataRecord?.MeasurementDate,
                    dataRecord?.StationInfo.StationCode,
                    dataRecord?.AirPollutionInfo
                })
                .UseUri(_uriBuilder.Build())
                .Build();

            using(var client = _clientFactory.CreateClient())
            {
                response = await client.SendAsync(request);
            }
            
            if(response.IsSuccessStatusCode)
                _logger.LogInformation("Data record successfully processed by analytics.");
            else
                _logger.LogWarning("Analytics failed to process data record.");
        }
    }
}