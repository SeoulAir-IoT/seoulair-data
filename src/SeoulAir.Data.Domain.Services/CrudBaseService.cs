using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Domain.Interfaces.Services;
using SeoulAir.Data.Domain.Services.Extensions;
using System;
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

        public async Task<PaginatedResultDto<TDto>> GetPaginated(Paginator paginator)
        {
            CheckTypeProperties(paginator);

            return await _baseRepository.GetPaginated(paginator);
        }

        public async Task UpdateAsync(TDto dto)
        {
            await _baseRepository.UpdateAsync(dto);
        }

        private void CheckTypeProperties(Paginator paginator)
        {
            if (!typeof(TDto).HasPublicProperty(paginator.OrderBy))
                throw new ArgumentException($"Pagination error. Invalid \"Order By\" option: {paginator.OrderBy}");

            if (paginator.FilterBy != null && !typeof(TDto).HasPublicProperty(paginator.FilterBy))
                throw new ArgumentException($"Pagination error. Invalid \"Filter by\" option: {paginator.FilterBy}");
        }
    }
}
