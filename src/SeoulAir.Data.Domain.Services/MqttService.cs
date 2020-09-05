using SeoulAir.Data.Domain.Interfaces.Services;
using SeoulAir.Data.Domain.Dtos;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;

namespace SeoulAir.Data.Domain.Services
{
    public sealed class MqttService<TDto> : IMqttService<TDto>
        where TDto : class
    {
        private readonly MqttSettings _settings;
        private IMqttClient _mqttClient;

        public MqttService(MqttSettings settings)
        {
            _settings = settings;
        }
        public async Task CloseConnection()
        {
            if (!_mqttClient.IsConnected)
                return;

            await _mqttClient.DisconnectAsync();
            _mqttClient.Dispose();
            _mqttClient = null;
        }

        public void Dispose()
        {
            if (_mqttClient != null)
            {
                if (_mqttClient.IsConnected)
                    _mqttClient.DisconnectAsync();
                _mqttClient.Dispose();
            }
        }

        public async Task OpenConnection()
        {
            MqttFactory factory = new MqttFactory();
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(_settings.BrokerAddress, _settings.BrokerPort)
                .WithClientId("dataClient")
                .Build();

            _mqttClient = factory.CreateMqttClient();
            await _mqttClient.ConnectAsync(options);
        }

        public async Task SubscribeToTopic()
        {
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(_settings.SubscribeTopic)
                .Build());

           _mqttClient.UseApplicationMessageReceivedHandler(handler=> 
           {
               Console.WriteLine(" ----- Recieved message -----");
               Console.WriteLine("Message: "+handler.ApplicationMessage);
           });
        }

    }
}
