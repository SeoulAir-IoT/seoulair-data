using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using SeoulAir.Data.Domain.Builders;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Domain.Services.Builders
{
    public class MicroserviceHttpRequestBuilder: IMicroserviceHttpRequestBuilder
    {
        private HttpMethod _httpMethod;
        private StringContent _requestBody;
        private Uri _requestUri;

        public HttpRequestMessage Build()
        {
            ValidateParameters();
            HttpRequestMessage message = new HttpRequestMessage(_httpMethod, _requestUri);

            if (_httpMethod != default)
                message.Content = _requestBody;

            return message;
        }

        public IMicroserviceHttpRequestBuilder Restart()
        {
            _httpMethod = default;
            _requestBody = default;
            _requestUri = default;
            return this;
        }

        public IMicroserviceHttpRequestBuilder UseHttpMethod(HttpMethod method)
        {
            if (method == default)
                throw new ArgumentNullException(nameof(method));

            _httpMethod = method;
            return this;
        }

        public IMicroserviceHttpRequestBuilder UseRequestBody<TParameter>(TParameter parameter)
        {
            if (parameter.Equals(default(TParameter)))
                throw new ArgumentNullException(nameof(parameter));

            _requestBody = new StringContent(
                JsonSerializer.Serialize(parameter, typeof(TParameter)),
                Encoding.UTF8,
                "application/json");

            return this;
        }

        public IMicroserviceHttpRequestBuilder UseUri(Uri uri)
        {
            if (uri == default)
                throw new ArgumentNullException(nameof(uri));

            _requestUri = uri;
            return this;
        }
    
        private void ValidateParameters()
        {
            if (_httpMethod == null)
                throw new ArgumentNullException(string.Format(InvalidParameterValueMessage, nameof(_httpMethod)));

            if (_requestUri == null)
                throw new ArgumentNullException(string.Format(InvalidParameterValueMessage, nameof(_requestUri)));

            if (_httpMethod == HttpMethod.Get && _requestBody != default)
                throw new ArgumentException(RequestBodyGetException);
        }
    }
}
