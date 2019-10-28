using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Waes.CodeAssignment.ScalableWeb.Api.Entities;
using Waes.CodeAssignment.ScalableWeb.Api.Infrastructure;
using Waes.CodeAssignment.ScalableWeb.Api.Services;

namespace Waes.CodeAssignment.ScalableWeb.Api
{
    /// <summary>
    /// Base Startup class responsible for setting up the app's services and pipelines
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new Startup instance
        /// </summary>
        /// <param name="configuration">Set of key/value application configuration properties</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure the necessary application services and dependency container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ScalableWeb API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient(typeof(IBinaryDiffService), typeof(BinaryDiffService));
            services.AddTransient(typeof(IRepository<BinaryDiffComparisonEntity>), typeof(BinaryRepository));
            services.AddTransient<BinaryComparer>();
            services.BuildServiceProvider();
        }

        
        /// <summary>
        /// Configures the HTTP request pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScalableWeb API V1");
            });
            app.UseMvc();
        }
    }
}
