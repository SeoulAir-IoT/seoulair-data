using System;
using System.Threading.Tasks;

namespace SeoulAir.Data.Domain.Interfaces.Services
{
    public interface IMqttService<in TDto> :IDisposable 
        where TDto: class
    {
        Task SubscribeToTopic();
        Task OpenConnection();
        Task CloseConnection();
    }
}
