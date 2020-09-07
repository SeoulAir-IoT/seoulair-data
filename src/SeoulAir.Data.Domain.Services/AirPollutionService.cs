using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Domain.Interfaces.Services;

namespace SeoulAir.Data.Domain.Services
{
    public class AirPollutionService : CrudBaseService<DataRecordDto>, IAirPollutionService
    {
        public AirPollutionService(ICrudBaseRepository<DataRecordDto> baseRepository) : base(baseRepository) { }
    }
}
