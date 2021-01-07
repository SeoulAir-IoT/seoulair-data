using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Extensions;
using SeoulAir.Data.Domain.Options;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services.OptionsValidators
{
    public class MqttOptionsValidator : IValidateOptions<MqttConnectionOptions>
    {
        private const int PortMinNumber = 1024;
        private const int PortMaxNumber = 65535;

        public ValidateOptionsResult Validate(string name, MqttConnectionOptions options)
        {
            ICollection<string> failureMessages = new List<string>();

            if (string.IsNullOrEmpty(options.BrokerAddress))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.BrokerAddress))
                    .FormatAsExceptionMessage());

            if (string.IsNullOrEmpty(options.Topic))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.Topic))
                    .FormatAsExceptionMessage());

            if (options.BrokerPort < PortMinNumber || options.BrokerPort > PortMaxNumber)
                failureMessages.Add(string.Format(
                    ParameterBetweenMessage,
                    nameof(options.BrokerPort),
                    PortMinNumber,
                    PortMaxNumber));

            return failureMessages.Any() 
                ? ValidateOptionsResult.Fail(failureMessages) 
                : ValidateOptionsResult.Success;
        }

    }
}