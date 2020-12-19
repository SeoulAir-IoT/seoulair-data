using System.Collections.Generic;

namespace SeoulAir.Data.Domain.Dtos
{
    public class PaginatedResultDto<TDto> where TDto : BaseDtoWithId
    {
        public List<TDto> Result { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
    }
}
