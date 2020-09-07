using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;

namespace SeoulAir.Data.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAirPollutionService, AirPollutionService>();
            services.AddScoped<ICrudBaseService<DataRecordDto>, CrudBaseService<DataRecordDto>>();

            return services;
        }
    }
}
