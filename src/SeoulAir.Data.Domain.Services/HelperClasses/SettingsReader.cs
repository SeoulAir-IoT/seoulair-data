using Microsoft.Extensions.Configuration;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;

namespace SeoulAir.Data.Domain.Services.HelperClasses
{
    public class SettingsReader: ISettingsReader
    {
        private readonly IConfiguration _appConfiguration;

        public SettingsReader(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public MqttSettings ReadAllSettings()
        {
            MqttSettings mqttSettings = new MqttSettings();

            //if (!TryReadSetting("Mqtt:Broker.Address", out var tempHolder))
            //    throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Address"));

            //mqttSettings.BrokerAddress = tempHolder;

            //if (!TryReadSetting("Mqtt:Broker.Port", out tempHolder))
            //    throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Port"));

            //mqttSettings.BrokerPort = short.Parse(tempHolder);

            //if (!TryReadSetting("Mqtt:Topic", out tempHolder))
            //    throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Topic"));

            //mqttSettings.SubscribeTopic = tempHolder;

            return mqttSettings;
        }

        public bool TryReadSetting(string name, out string value)
        {
            value = _appConfiguration[name];

            return !string.IsNullOrWhiteSpace(value);
        }

    }
}
