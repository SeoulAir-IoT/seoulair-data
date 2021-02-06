using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Exceptions;
using SeoulAir.Data.Domain.Interfaces.Services;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Options;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services
{
    public sealed class MqttListenerService<TDto> : IMqttListenerService<TDto>
        where TDto : BaseDtoWithId
    {
        private readonly MqttConnectionOptions _settings;
        private readonly ICrudBaseService<TDto> _crudBaseService;
        private readonly ILogger<MqttListenerService<TDto>> _logger;
        private readonly IAnalyticsService _analyticsService;
        private IMqttClient _mqttClient;

        public MqttListenerService(IOptions<MqttConnectionOptions> settings, ICrudBaseService<TDto> crudBaseService,
            ILogger<MqttListenerService<TDto>> logger, IAnalyticsService analyticsService)
        {
            _settings = settings.Value;
            _crudBaseService = crudBaseService;
            _logger = logger;
            _analyticsService = analyticsService;
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
            if (_mqttClient == null) 
                return;
            
            if (_mqttClient.IsConnected)
                _mqttClient.DisconnectAsync();
            _mqttClient.Dispose();
        }

        public bool IsConnected()
        {
            return _mqttClient != default && _mqttClient.IsConnected;
        }

        public async Task OpenConnection()
        {
            if(_mqttClient != null && _mqttClient.IsConnected)
            {
                _logger.LogWarning(MqttClientConnectionWarning);
                return;
            }

            MqttFactory factory = new MqttFactory();
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithTcpServer(_settings.BrokerAddress, _settings.BrokerPort)
                .WithCommunicationTimeout(TimeSpan.FromSeconds(6))
                .WithClientId(_settings.DataReceiverClientId)
                .Build();

            _mqttClient = factory.CreateMqttClient();

            try
            {
                await _mqttClient.ConnectAsync(options);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
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
            TDto result = DeserializeObject(messageArgs.ApplicationMessage.Payload);
            await _crudBaseService.AddAsync(result);
            Task.Run(() => _analyticsService.SendDataToAnalyticsService(result));
        }

        private TDto DeserializeObject(byte[] messagePayload)
        {
            string objectAsString = Encoding.UTF8.GetString(messagePayload);
            TDto result;
            try
            {
                result = JsonSerializer.Deserialize<TDto>(objectAsString);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw new MessageConvertException(ReadingMessageExceptionMessage);
            }
            return result;
        }
    }
}
