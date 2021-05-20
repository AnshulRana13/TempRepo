using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
/// <summary>
/// Polly 
/// </summary>
namespace AssignmentDemo.API.Middleware
{
    public static class PollyContainerService
    {
        public static void AddPollyContainerService(this IServiceCollection services)
        {
            services.AddHttpClient("AssignmentDemo")
           .SetHandlerLifetime(TimeSpan.FromMinutes(5))
           .AddPolicyHandler(GetRetryPolicy())
           .AddPolicyHandler(GetCircuitBreakerPolicy());


        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
       // HttpRequestException, 5XX and 408  
       .HandleTransientHttpError()
       // 404  
       //.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
       // Retry three times after delay  
       .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));  //After two times falls it will not retry until 30 sec.
        }
    }
}
