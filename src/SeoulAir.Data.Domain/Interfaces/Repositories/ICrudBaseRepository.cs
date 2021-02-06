﻿using SeoulAir.Data.Domain.Dtos;
using System.Threading.Tasks;

namespace SeoulAir.Data.Domain.Interfaces.Repositories
{
    public interface ICrudBaseRepository<TDto>
        where TDto : BaseDtoWithId
    {
        Task<string> AddAsync(TDto dto);
        Task UpdateAsync(TDto dto);
        Task DeleteAsync(string id);
        Task<TDto> GetByIdAsync(string id);
        Task<PaginatedResultDto<TDto>> GetPaginated(Paginator paginator);
    }
}
