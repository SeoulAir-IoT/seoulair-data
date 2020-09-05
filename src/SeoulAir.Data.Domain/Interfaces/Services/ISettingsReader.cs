using SeoulAir.Data.Domain.Dtos;

namespace SeoulAir.Data.Domain.Interfaces.Services
{
    public interface ISettingsReader
    {
        MqttSettings ReadAllSettings();

        bool TryReadSetting(string name, out string value);
    }
}
