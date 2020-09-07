using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace SeoulAir.Data.Domain.Services
{
    public class CrudBaseService<TDto> : ICrudBaseService<TDto>
        where TDto : BaseDtoWithId
    {
        protected readonly ICrudBaseRepository<TDto> _baseRepository;

        public CrudBaseService(ICrudBaseRepository<TDto> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddAsync(TDto dto)
        {
            await _baseRepository.AddAsync(dto);
        }

        public async Task DeleteAsync(string id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task<TDto> GetById(string id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TDto dto)
        {
            await _baseRepository.UpdateAsync(dto);
        }
    }
}
