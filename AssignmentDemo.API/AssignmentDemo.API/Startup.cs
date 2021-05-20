using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentDemo.API.Middleware;
using AssignmentDemo.Entities.API.AppConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDemo.API
{
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        private readonly IWebHostEnvironment environment;
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="_environment"></param>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration,IWebHostEnvironment _environment)
        {
            Configuration = configuration;
            environment = _environment;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            AppSettings.setConfiguration(this.Configuration);
           
            services.AddMemoryCache();
            services.AddCorsPolicy();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDependencyInjectionContainer(this.Configuration);
            services.AddPollyContainerService();
            services.AddAppVersioning();
            services.AddSwaggerServiceConfiguration(Configuration, environment);
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                   new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters()
           .ConfigureApiBehaviorOptions(setupAction =>
           {
               setupAction.InvalidModelStateResponseFactory = context =>
               {
                   // create a problem details object
                   var problemDetailsFactory = context.HttpContext.RequestServices
                       .GetRequiredService<ProblemDetailsFactory>();
                   var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                           context.HttpContext,
                           context.ModelState);

                   // add additional info not added by default
                   problemDetails.Detail = "See the errors field for details.";
                   problemDetails.Instance = context.HttpContext.Request.Path;

                   // find out which status code to use
                   var actionExecutingContext =
                         context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                   // if there are modelstate errors & all keys were correctly
                   // found/parsed we're dealing with validation errors
                   if ((context.ModelState.ErrorCount > 0) &&
                       (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                   {
                       problemDetails.Type = "Model State is Corrupted";
                       problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                       problemDetails.Title = "One or more validation errors occurred.";

                       return new UnprocessableEntityObjectResult(problemDetails)
                       {
                           ContentTypes = { "application/problem+json" }
                       };
                   }

                   // if one of the keys wasn't correctly found / couldn't be parsed
                   // we're dealing with null/unparsable input
                   problemDetails.Status = StatusCodes.Status400BadRequest;
                   problemDetails.Title = "One or more errors on input occurred.";
                   return new BadRequestObjectResult(problemDetails)
                   {
                       ContentTypes = { "application/problem+json" }
                   };
               };
         

           });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
          
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandlerService();
            app.UseCors(CorsConfigurationService.ALLOW_ALL_ORIGINS_POLICY);
            app.UseSwaggerUI();
           
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
           
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
