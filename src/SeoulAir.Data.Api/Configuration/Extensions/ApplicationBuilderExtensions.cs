using Microsoft.AspNetCore.Builder;
using static SeoulAir.Data.Domain.Resources.Strings;

namespace SeoulAir.Data.Api.Configuration.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder  UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint(string.Format(SwaggerEndpoint, OpenApiInfoProjectVersion),
                    OpenApiInfoProjectVersion);
                config.RoutePrefix = string.Empty;
                config.DocumentTitle = OpenApiInfoTitle;
            });
            app.UseSwagger();
            
            return app;
        }
    }
}
