using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Data.Domain.Builders;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using SeoulAir.Data.Domain.Services.Builders;

namespace SeoulAir.Data.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IAirPollutionService, AirPollutionService>();
            services.AddSingleton<ICrudBaseService<DataRecordDto>, CrudBaseService<DataRecordDto>>();
            services.AddSingleton<IMqttListenerService<DataRecordDto>,MqttListenerService<DataRecordDto>>();

            services.AddSingleton<IMicroserviceHttpRequestBuilder, MicroserviceHttpRequestBuilder>();
            services.AddSingleton<IMicroserviceUriBuilder, MicroserviceUriBuilder>();
            services.AddSingleton<IAnalyticsService, AnalyticsService>();
            return services;
        }
    }
}
