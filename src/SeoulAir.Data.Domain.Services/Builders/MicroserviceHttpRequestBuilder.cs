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
        private HttpMethod HttpMethod;
        private StringContent RequestBody;
        private Uri RequestUri;

        public HttpRequestMessage Build()
        {
            ValidateParameters();
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod, RequestUri);

            if (HttpMethod != default)
                message.Content = RequestBody;

            return message;
        }

        public IMicroserviceHttpRequestBuilder Restart()
        {
            HttpMethod = default;
            RequestBody = default;
            RequestUri = default;
            return this;
        }

        public IMicroserviceHttpRequestBuilder UseHttpMethod(HttpMethod method)
        {
            if (method == default)
                throw new ArgumentNullException(nameof(method));

            HttpMethod = method;
            return this;
        }

        public IMicroserviceHttpRequestBuilder UseRequestBody<TParameter>(TParameter parameter)
        {
            if (parameter.Equals(default(TParameter)))
                throw new ArgumentNullException(nameof(parameter));

            RequestBody = new StringContent(
                JsonSerializer.Serialize(parameter, typeof(TParameter)),
                Encoding.UTF8,
                "application/json");

            return this;
        }

        public IMicroserviceHttpRequestBuilder UseUri(Uri uri)
        {
            if (uri == default)
                throw new ArgumentNullException(nameof(uri));

            RequestUri = uri;
            return this;
        }
    
        private void ValidateParameters()
        {
            if (HttpMethod == null)
                throw new ArgumentNullException(string.Format(InvalidParameterValueMessage, nameof(HttpMethod)));

            if (RequestUri == null)
                throw new ArgumentNullException(string.Format(InvalidParameterValueMessage, nameof(RequestUri)));

            if (HttpMethod == HttpMethod.Get && RequestBody != default)
                throw new ArgumentException(RequestBodyGetException);
        }
    }
}
