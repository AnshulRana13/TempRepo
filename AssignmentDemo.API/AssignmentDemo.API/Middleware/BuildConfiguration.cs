using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Middleware
{
    public static class BuildConfiguration
    {
        public static IConfiguration AddBuildConfiguration(string environment)
        {
            return new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .AddJsonFile($"appsettings.{environment ?? string.Empty}.json", true, true)
              .Build();
        }
    }
}
