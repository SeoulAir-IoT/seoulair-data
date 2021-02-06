using System.Threading.Tasks;

namespace SeoulAir.Data.Domain.Interfaces.Services
{
    public interface IAnalyticsService
    {
        Task SendDataToAnalyticsService<TDto>(TDto dataRecord);
    }
}
