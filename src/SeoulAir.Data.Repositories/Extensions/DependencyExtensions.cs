using Microsoft.Extensions.DependencyInjection;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Repositories;
using SeoulAir.Data.Repositories.Entities;

namespace SeoulAir.Device.Domain.Services.Extensions
{
    public static class DependencyExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IAirPollutionRepository, AirPollutionRepository>();
            services.AddSingleton<ICrudBaseRepository<DataRecordDto>, CrudBaseRepository<DataRecordDto, DataRecord>>();
            services.AddSingleton<IMongoDbContext, MongoDbContext>();

            return services;
        }
    }
}
