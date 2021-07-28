using AssignmentDemo.Entities.API.AlbumDetails;
using AssignmentDemo.Entities.API.PhotoDetails;
using AssignmentDemo.Entities.API.UserDetails;
using AssignmentDemo.Provider.AlbumRequest;
using AssignmentDemo.Provider.Cache;
using AssignmentDemo.Provider.PhotoRequest;
using AssignmentDemo.Provider.UserRequest;
using AssignmentDemo.Provider.WebClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Middleware
{
    /// <summary>
    /// Add Dependency Injection Container
    /// </summary>
    public static class DependancyInjectionContainerService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDependencyInjectionContainer(this IServiceCollection services,IConfiguration configuration)
        {


            var cacheManagerType = configuration.GetValue<string>("cacheManagerType");
            switch(cacheManagerType)
            {
                case "InMemory":
                      services.AddSingleton<ICacheManager, InMemoryCacheManager>();
                      break;
                case "Redis":
                    services.AddSingleton<ICacheManager, RedisCacheManager>();
                    break;
                default:
                    throw new NotImplementedException();
            }
            services.AddTransient<IWebRequestHandler<User>, WebRequestHandler<User>>();
            services.AddTransient<IWebRequestHandler<Photo>, WebRequestHandler<Photo>>();
            services.AddTransient<IWebRequestHandler<Album>, WebRequestHandler<Album>>();
            services.AddTransient<IUserRequestHandler, UserRequestHandler>();
            services.AddTransient<IAlbumRequestHandler, AlbumRequestHandler>();
            services.AddTransient<IPhotoRequestHandler, PhotoRequestHandler>();
        }
    }
}
