using Api.Entitys;
using Api.Services;
using Api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();

            services.AddAutoMapper(typeof(Startup));
            services.Configure<MQConfig>(_configuration.GetSection("MQConfig"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICryptoManager, CryptoManager>();
            services.AddSingleton<IFileManager, FileManager>();
            services.AddSingleton<IMQBroker, MqBrokerFile>();
            services.AddSingleton<ICustomMemoryCache, CustomMemoryCache>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostAppLifetime)
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

            hostAppLifetime.ApplicationStarted.Register(() =>  {
                var fileManager = app.ApplicationServices.GetService<IFileManager>();
                fileManager.CreateFile();
            });

            hostAppLifetime.ApplicationStopped.Register(() =>  {
                var fileManager = app.ApplicationServices.GetService<IFileManager>();
                fileManager.DeleteFiles();
            });
        }

    }
}
