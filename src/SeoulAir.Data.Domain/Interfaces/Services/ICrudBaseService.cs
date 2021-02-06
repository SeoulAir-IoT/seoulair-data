using SeoulAir.Data.Domain.Dtos;
using System.Threading.Tasks;

namespace SeoulAir.Data.Domain.Interfaces.Services
{
    public interface ICrudBaseService<TDto>
        where TDto : BaseDtoWithId
    {
        Task<string> AddAsync(TDto dto);
        Task DeleteAsync(string id);
        Task UpdateAsync(TDto dto);
        Task<TDto> GetById(string id);
        Task<PaginatedResultDto<TDto>> GetPaginated(Paginator paginator);
    }
}
