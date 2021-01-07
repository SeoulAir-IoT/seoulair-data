using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Options;
using SeoulAir.Data.Domain.Services.OptionsValidators;

namespace SeoulAir.Data.Api.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MqttConnectionOptions>(configuration.GetSection(MqttConnectionOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<MqttConnectionOptions>, MqttOptionsValidator>();

            services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<MongoDbOptions>, MongoDbOptionsValidator>();
            
            return services;
        }
    }
}

