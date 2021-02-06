using System;
using System.Net.Http;

namespace SeoulAir.Data.Domain.Builders
{
    public interface IMicroserviceHttpRequestBuilder
    {
        IMicroserviceHttpRequestBuilder UseUri(Uri uri);
        IMicroserviceHttpRequestBuilder UseHttpMethod(HttpMethod method);
        IMicroserviceHttpRequestBuilder UseRequestBody<TParameter>(TParameter parameter);
        IMicroserviceHttpRequestBuilder Restart();
        HttpRequestMessage Build();
    }
}
