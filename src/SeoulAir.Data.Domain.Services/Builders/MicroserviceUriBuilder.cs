using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using SeoulAir.Data.Domain.Builders;
using SeoulAir.Data.Domain.Options;
using SeoulAir.Data.Domain.Resources;

namespace SeoulAir.Data.Domain.Services.Builders
{
    public class MicroserviceUriBuilder : IMicroserviceUriBuilder
    {
        private readonly Dictionary<string, string> _queryParameters;
        private readonly List<string> _pathParameters;
        private string _endpoint;
        private string _controllerName;
        private MicroserviceUrlOptions _microserviceUrlOptions;

        public MicroserviceUriBuilder()
        {
            _queryParameters = new Dictionary<string, string>();
            _pathParameters = new List<string>();
        }

        public MicroserviceUriBuilder(MicroserviceUrlOptions options) : this()
        {
            _microserviceUrlOptions = options;
        }

        public IMicroserviceUriBuilder AddQueryParameter<TParameter>(string parameterName, TParameter value)
        {
            if (parameterName == default)
                throw new ArgumentNullException(nameof(parameterName));

            if (value.Equals(default(TParameter)))
                throw new ArgumentNullException(nameof(value));

            _queryParameters.Add(parameterName, value.ToString());
            return this;
        }

        public IMicroserviceUriBuilder AddPathParameter(string value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value));

            _pathParameters.Add(value);

            return this;
        }

        public Uri Build()
        {
            ValidateProperties();

            UriBuilder builder = new UriBuilder();
            builder.Scheme = "http";
            builder.Host = _microserviceUrlOptions.Address;
            builder.Port = _microserviceUrlOptions.Port;
            builder.Path = BuildPath();
            builder.Query = BuildQuery();

            return builder.Uri;
        }

        public IMicroserviceUriBuilder Restart()
        {
            _queryParameters.Clear();
            _pathParameters.Clear();
            _endpoint = default;
            _controllerName = default;
            _microserviceUrlOptions = default;
            return this;
        }

        public IMicroserviceUriBuilder SetEndpoint(string endpoint)
        {
            if (endpoint == default)
                throw new ArgumentNullException(nameof(endpoint));

            _endpoint = endpoint.ToLower().Trim();
            return this;
        }

        public IMicroserviceUriBuilder UseController(string controllerName)
        {
            if (controllerName == default)
                throw new ArgumentNullException(nameof(controllerName));

            _controllerName = controllerName.ToLower().Trim();
            return this;
        }

        public IMicroserviceUriBuilder UseMicroserviceUrlOptions(MicroserviceUrlOptions microserviceOptions)
        {
            if (microserviceOptions == default)
                throw new ArgumentNullException(nameof(microserviceOptions));

            if (microserviceOptions.Address == default || microserviceOptions.Port == default)
                throw new ArgumentException(string.Format(Strings.InvalidParameterValueMessage, nameof(microserviceOptions)));

            _microserviceUrlOptions = microserviceOptions;
            return this;
        }

        private string BuildPath()
        {
            StringBuilder path = new StringBuilder("/");
            path.Append(_controllerName);

            if (!string.IsNullOrEmpty(_endpoint))
            {
                path.Append("/");
                path.Append(_endpoint);
            }

            foreach(string parameter in _pathParameters)
            {
                path.Append("/");
                path.Append(parameter);
            }

            return path.ToString();
        }

        private string BuildQuery()
        {
            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

            foreach(var nameValue in _queryParameters)
                queryString.Add(nameValue.Key, nameValue.Value);

            return queryString.ToString();
        }

        private void ValidateProperties()
        {
            if (_microserviceUrlOptions == default)
                throw new ArgumentNullException(nameof(_microserviceUrlOptions));

            if (_controllerName == default)
                throw new ArgumentNullException(nameof(_controllerName));
        }
    }
}
