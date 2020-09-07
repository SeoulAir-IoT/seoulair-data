using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeoulAir.Data.Api.Configuration;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Services.Extensions;
using SeoulAir.Device.Domain.Services.Extensions;
using SeoulAir.Data.Api.Configuration.Extensions;

namespace SeoulAir.Data.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMongoDb(Configuration);

            services.AddControllers();

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddDomainServices();

            services.AddRepositories();

            services.AddSwagger();

            services.AddMQTT(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerDocumentation();
        }
    }
}
