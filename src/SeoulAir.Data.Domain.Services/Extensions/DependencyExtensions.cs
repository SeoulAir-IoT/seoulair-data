using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;

namespace SeoulAir.Data.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IAirPollutionService, AirPollutionService>();
            services.AddSingleton<ICrudBaseService<DataRecordDto>, CrudBaseService<DataRecordDto>>();
            services.AddSingleton<IMqttListenerService<DataRecordDto>,MqttListenerService<DataRecordDto>>();

            return services;
        }
    }
}
