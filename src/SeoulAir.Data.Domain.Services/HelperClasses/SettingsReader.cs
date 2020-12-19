using Microsoft.Extensions.Configuration;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Exceptions;
using SeoulAir.Data.Domain.Interfaces.Services;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services.HelperClasses
{
    public class SettingsReader: ISettingsReader
    {
        private readonly IConfiguration _appConfiguration;

        public SettingsReader(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public AppSettings ReadAllSettings()
        {
            AppSettings result = new AppSettings()
            {
                mqttSettings = ReadMqttSettings(),
                mongoDbSettings = ReadMongoSettings()
            };

            return result;
        }

        public bool TryReadSetting(string name, out string value)
        {
            value = _appConfiguration[name];

            return !string.IsNullOrWhiteSpace(value);
        }

        private MqttSettings ReadMqttSettings()
        {
            MqttSettings mqttSettings = new MqttSettings();

            if (!TryReadSetting("Mqtt:Broker.Address", out var tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Address"));

            mqttSettings.BrokerAddress = tempHolder;

            if (!TryReadSetting("Mqtt:Broker.Port", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Port"));

            mqttSettings.BrokerPort = short.Parse(tempHolder);

            if (!TryReadSetting("Mqtt:Topic", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Topic"));

            mqttSettings.SubscribeTopic = tempHolder;

            return mqttSettings;
        }

        private MongoDbSettings ReadMongoSettings()
        {
            MongoDbSettings mongoSettings = new MongoDbSettings();

            if (!TryReadSetting("MongoDbSettings:ConnectionString", out var tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Address"));

            mongoSettings.ConnectionString = tempHolder;

            if (!TryReadSetting("MongoDbSettings:DatabaseName", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Broker.Port"));

            mongoSettings.DatabaseName = tempHolder;

            if (!TryReadSetting("MongoDbSettings:Username", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Topic"));

            mongoSettings.Username = tempHolder;

            if (!TryReadSetting("MongoDbSettings:Password", out tempHolder))
                throw new ConfigurationException(string.Format(InvalidConfigurationAttribute, "Mqtt:Topic"));

            mongoSettings.Password = tempHolder;

            return mongoSettings;
        }
    }
}
