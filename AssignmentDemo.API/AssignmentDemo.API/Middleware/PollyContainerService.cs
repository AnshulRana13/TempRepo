using AssignmentDemo.Provider.AlbumRequest;
using AssignmentDemo.Provider.PhotoRequest;
using AssignmentDemo.Provider.UserRequest;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Middleware
{
    public static class PollyContainerService
    {
        public static void AddPollyContainerService(this IServiceCollection services)
        {
         services.AddHttpClient<IAlbumRequestHandler, AlbumRequestHandler>()
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

         services.AddHttpClient<IPhotoRequestHandler, PhotoRequestHandler>()
         .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
         .AddPolicyHandler(GetRetryPolicy())
         .AddPolicyHandler(GetCircuitBreakerPolicy());

         services.AddHttpClient<IUserRequestHandler, UserRequestHandler>()
        .SetHandlerLifetime(TimeSpan.FromMinutes(5))  //Set lifetime to five minutes
        .AddPolicyHandler(GetRetryPolicy())
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        }


        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)); //Retry 3 times after every 2 seconds
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));  //After five times falls it will not retry until 30 sec.
        }
    }
}
