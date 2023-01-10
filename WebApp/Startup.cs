using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen
             (c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Version = AppSettings.Environment,
                     Title = $"{nameof(WebApp)} API",
                     Description = "This API can get data from database of: Birthday list, Latest Buyers, Popular categories. (directly)",


                     Contact = new OpenApiContact
                     {
                         Name = "Code Repository",
                     //    Url = new Uri("https://")
                     }
                 }); ;


                 // XML Documentation
                 var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                 var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                 c.IncludeXmlComments(xmlPath);
             });
            /*
            services.AddControllers()
              .AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore
               );
            */
            SetConfigByEnvirmoment(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApp v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }

        public IServiceCollection SetConfigByEnvirmoment(IServiceCollection services) 
        {
            AppSettings.Environment = Configuration.GetSection("Environment").Value;
            AppSettings.AppName = nameof(WebApp);

            ConfigInterfaceRun.testDB = Configuration.GetSection("ConnectionStrings").GetSection("TestDB").Value;
            ConfigInterfaceRun.BirthdayList = Configuration.GetSection("BirthdayList").Value;
            ConfigInterfaceRun.BuyersList = Configuration.GetSection("BuyersList").Value;

            ConfigInterfaceRun.CathegoriesList = Configuration.GetSection("CathegoriesList").Value;

            return services;
        }
    }
}
