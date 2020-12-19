using SeoulAir.Data.Domain.Dtos;

namespace SeoulAir.Data.Domain.Interfaces.Services
{
    public interface ISettingsReader
    {
        AppSettings ReadAllSettings();

        bool TryReadSetting(string name, out string value);
    }
}
