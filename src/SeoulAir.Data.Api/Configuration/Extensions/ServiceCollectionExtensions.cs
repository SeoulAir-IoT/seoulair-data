using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SeoulAir.Data.Domain.Options;
using SeoulAir.Data.Domain.Services.OptionsValidators;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Api.Configuration.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                var xmlDocumentationFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocumentationFileName));
                options.DescribeAllParametersInCamelCase();
                options.SwaggerDoc(OpenApiInfoProjectVersion, new OpenApiInfo
                {
                    Title = OpenApiInfoTitle,
                    Description = OpenApiInfoDescription,
                    Version = OpenApiInfoProjectVersion,
                    Contact = new OpenApiContact
                    {
                        Email = string.Empty,
                        Name = GitlabContactName,
                        Url = new Uri(GitlabRepoUri)
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddApplicationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<MqttConnectionOptions>(configuration.GetSection(MqttConnectionOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<MqttConnectionOptions>, MqttOptionsValidator>();

            services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<MongoDbOptions>, MongoDbOptionsValidator>();
            
            services.Configure<SeoulAirAnalyticsOptions>(
                configuration.GetSection(SeoulAirAnalyticsOptions.AppSettingsPath));
            services.AddSingleton<IValidateOptions<SeoulAirAnalyticsOptions>, SeoulAirAnalyticsValidator>();
            
            return services;
        }
    }
}
