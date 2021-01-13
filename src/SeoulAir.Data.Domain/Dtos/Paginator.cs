namespace SeoulAir.Data.Domain.Dtos
{
    public class Paginator
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string OrderBy { get; set; } = "Id";
        public bool IsDescending { get; set; } = false;
        public string FilterBy { get; set; } = null;
        public string FilterValue { get; set; } = null;
        public FilterType? FilterType { get; set; } = null;
    }
}
