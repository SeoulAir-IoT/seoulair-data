using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Domain.Interfaces.Services;
using SeoulAir.Data.Domain.Services.Extensions;
using System;
using System.Threading.Tasks;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services
{
    public class CrudBaseService<TDto> : ICrudBaseService<TDto>
        where TDto : BaseDtoWithId
    {
        protected readonly ICrudBaseRepository<TDto> BaseRepository;

        public CrudBaseService(ICrudBaseRepository<TDto> baseRepository)
        {
            BaseRepository = baseRepository;
        }

        public async Task<string> AddAsync(TDto dto)
        {
            return await BaseRepository.AddAsync(dto);
        }

        public async Task DeleteAsync(string id)
        {
            await BaseRepository.DeleteAsync(id);
        }

        public async Task<TDto> GetById(string id)
        {
            return await BaseRepository.GetByIdAsync(id);
        }

        public async Task<PaginatedResultDto<TDto>> GetPaginated(Paginator paginator)
        {
            CheckTypeProperties(paginator);

            return await BaseRepository.GetPaginated(paginator);
        }

        public async Task UpdateAsync(TDto dto)
        {
            await BaseRepository.UpdateAsync(dto);
        }

        private void CheckTypeProperties(Paginator paginator)
        {
            if (!typeof(TDto).HasPublicProperty(paginator.OrderBy))
                throw new ArgumentException(string.Format(PaginationOrderError, paginator.OrderBy));

            if (paginator.FilterBy != null && !typeof(TDto).HasPublicProperty(paginator.FilterBy))
                throw new ArgumentException(string.Format(PaginationFilterError, paginator.FilterBy));
        }
    }
}
