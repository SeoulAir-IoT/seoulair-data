using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Options;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services.OptionsValidators
{
    public class MongoDbOptionsValidator : IValidateOptions<MongoDbOptions>
    {
        public ValidateOptionsResult Validate(string name, MongoDbOptions options)
        {
            List<string> failureMessages = new List<string>();
            
            if (string.IsNullOrWhiteSpace(options.Password))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.Password)));
            
            if (string.IsNullOrWhiteSpace(options.Username))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.Username)));
            
            if (string.IsNullOrWhiteSpace(options.DatabaseName))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.DatabaseName)));
            
            if (string.IsNullOrWhiteSpace(options.ConnectionString))
                failureMessages.Add(string.Format(ParameterNullOrEmptyMessage, nameof(options.ConnectionString)));
            
            return failureMessages.Any() 
                ? ValidateOptionsResult.Fail(failureMessages) 
                : ValidateOptionsResult.Success;
        }
    }
}
