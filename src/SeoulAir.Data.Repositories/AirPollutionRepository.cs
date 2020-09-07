using AutoMapper;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Repositories.Entities;

namespace SeoulAir.Data.Repositories
{
    public class AirPollutionRepository : CrudBaseRepository<DataRecordDto, DataRecord>, IAirPollutionRepository
    {
        public AirPollutionRepository(IMapper mapper, IMongoDbContext dbContext) : base(mapper, dbContext) { }
    }
}
