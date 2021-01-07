using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using Newtonsoft.Json;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Exceptions;
using SeoulAir.Data.Domain.Interfaces.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Options;
using SeoulAir.Data.Domain.Services.OptionsValidators;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services
{
    public sealed class MqttListenerService<TDto> : IMqttListenerService<TDto>
        where TDto : BaseDtoWithId
    {
        private readonly MqttConnectionOptions _settings;
        private readonly ICrudBaseService<TDto> _crudBaseService;
        private IMqttClient _mqttClient;

        public MqttListenerService(IOptions<MqttConnectionOptions> settings, ICrudBaseService<TDto> crudBaseService)
        {
            _settings = settings.Value;
            _crudBaseService = crudBaseService;
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

        public bool IsConnected()
        {
            if (_mqttClient == default)
                return false;
            return _mqttClient.IsConnected;
        }

        public async Task OpenConnection()
        {
            if(_mqttClient != null && _mqttClient.IsConnected)
            {
                Console.WriteLine("Warning: Client already connected!");
                return;
            }

            MqttFactory factory = new MqttFactory();
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(_settings.BrokerAddress, _settings.BrokerPort)
                .WithCommunicationTimeout(TimeSpan.FromSeconds(6))
                .WithClientId(_settings.DataReceiverClientId)
                .Build();

            _mqttClient = factory.CreateMqttClient();
            _mqttClient.UseConnectedHandler(ShowConnectedInformation);
            _mqttClient.UseDisconnectedHandler(ShowDisconnectedInformation);

            try
            {
                await _mqttClient.ConnectAsync(options);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new MqttConnectionException(MqttConnectingExceptionMessage);
            }
        }

        public async Task SubscribeToTopic()
        {
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder()
                .WithTopic(_settings.Topic)
                .Build());

           _mqttClient.UseApplicationMessageReceivedHandler(ReceiveMessage);
        }

        private async Task ReceiveMessage(MqttApplicationMessageReceivedEventArgs messageArgs)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Message received");
            Console.WriteLine("-----------------------");
            TDto result = DeserializeObject(messageArgs.ApplicationMessage.Payload);
            Console.WriteLine("-----------------------");
            Console.WriteLine("Message deserialized");
            Console.WriteLine("-----------------------");
            await _crudBaseService.AddAsync(result);
            Console.WriteLine("-----------------------");
            Console.WriteLine("Message inserted in mongo");
            Console.WriteLine("-----------------------");
        }

        private TDto DeserializeObject(byte[] messagePayload)
        {
            string objectAsString = Encoding.UTF8.GetString(messagePayload);
            TDto result;
            try
            {
                result = JsonConvert.DeserializeObject<TDto>(objectAsString);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new MessageConvertException(ReadingMessageExceptionMessage);
            }
            return result;
        }
    
        private void ShowConnectedInformation(MqttClientConnectedEventArgs args)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Contected to the server!");
            Console.WriteLine($"Adress: {_settings.BrokerAddress}");
            Console.WriteLine($"Port: {_settings.BrokerPort}");
            Console.WriteLine("-----------------------");
        }

        private void ShowDisconnectedInformation(MqttClientDisconnectedEventArgs args)
        {
            Console.WriteLine("-----------------------");
            Console.WriteLine("Disconnected from the server!");
            Console.WriteLine($"Adress: {_settings.BrokerAddress}");
            Console.WriteLine($"Port: {_settings.BrokerPort}");
            Console.WriteLine("-----------------------");
        }
    }
}
