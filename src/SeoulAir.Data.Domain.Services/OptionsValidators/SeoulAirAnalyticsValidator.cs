using Microsoft.Extensions.Options;
using SeoulAir.Data.Domain.Options;

namespace SeoulAir.Data.Domain.Services.OptionsValidators
{
    public class SeoulAirAnalyticsValidator : MicroserviceUrlValidator, IValidateOptions<SeoulAirAnalyticsOptions>
    {
        public ValidateOptionsResult Validate(string name, SeoulAirAnalyticsOptions options)
        {
            return base.Validate(name, options);
        }
    }
}
