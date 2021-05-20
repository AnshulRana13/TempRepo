using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Middleware
{
    public static class SwaggerService
    {
        /// <summary>
        /// Swagger Configure Services
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerServiceConfiguration(this IServiceCollection services, IConfiguration configuration,IWebHostEnvironment env)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Description = "AssignmentDemo.API",
                    Title = $"AssignmentDemo.API - {env.EnvironmentName}"
                });

                c.SwaggerDoc("v2", new OpenApiInfo()
                {
                    Version = "v2",
                    Description = "AssignmentDemo.API",
                    Title = $"AssignmentDemo.API - {env.EnvironmentName}"
                });

                c.OperationFilter<SecurityRequirementsOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Document.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// Swagger Configuration For Application Builder
        /// </summary>
        /// <param name="app"></param>
        
        public static void UseSwaggerUI(this IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("v1/swagger.json", "V1");
                c.SwaggerEndpoint("v2/swagger.json", "V2");
            });
        }
    }
}
