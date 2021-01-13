using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Data.Domain.Exceptions;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Api.Configuration.Extensions
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var problemDetails = GenerateProblemDetails(ex);
            var jsonSetting = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var result = JsonSerializer.Serialize(problemDetails, jsonSetting);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)problemDetails.Status;

            return context.Response.WriteAsync(result);
        }

        private static ProblemDetails GenerateProblemDetails(Exception ex)
        {
            string type;
            string title;
            HttpStatusCode code;

            switch (ex)
            {
                case ConfigurationException _:
                case InvalidStationCodeFormatException _:
                case MqttConnectionException _:
                    code = HttpStatusCode.InternalServerError;
                    type = InternalServerErrorUri;
                    title = InternalServerErrorTitle;
                    Console.Write(ex.ToString());
                    break;
                default:
                    code = HttpStatusCode.NotImplemented;
                    type = NotImplementedUri;
                    title = NotImplementedTitle;
                    Console.Write(ex.ToString());
                    break;
            }

            return new ProblemDetails()
            {
                Type = type,
                Title = title,
                Detail = ex.Message,
                Status = (int)code
            };
        }
    }
}
