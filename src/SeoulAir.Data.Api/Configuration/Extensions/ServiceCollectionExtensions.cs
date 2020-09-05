using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using SeoulAir.Data.Domain.Services;
using SeoulAir.Data.Domain.Services.HelperClasses;

namespace SeoulAir.Data.Api.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddMQTT(this IServiceCollection services, IConfiguration configuration)
        {
            ISettingsReader reader = new SettingsReader(configuration);
            IMqttService<RawDataInstanceDto> _mqttService = new MqttService<RawDataInstanceDto>(reader.ReadAllSettings());

            /*await _mqttService.OpenConnection();
            while (IsOn)
            {
                await _mqttService.SubscribeToTopic();
            }
            await _mqttService.CloseConnection();
            */

            services.AddSingleton(_mqttService);
            return services;
        }
    }
}

