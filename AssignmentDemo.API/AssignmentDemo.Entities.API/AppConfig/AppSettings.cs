
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentDemo.Entities.API.AppConfig
{
    public class AppSettings
    {
        public static string ConnectionString_Redis { get; private set; }
        public static string UsersApiUrl { get; private set; }
        public static string AlbumsApiUrl { get; private set; }
        public static string PhotosApiUrl { get; private set; }
        public static string UsersKey { get; private set; }
        public static string PhotosKey { get; private set; }
        public static string AlbumsKey{ get; private set; }
        public static void setConfiguration(IConfiguration configuration)
        {
            ConnectionString_Redis = configuration["ConnectionString:Redis"];
            UsersApiUrl = configuration["usersApiUrl"];
            AlbumsApiUrl = configuration["albumApiUrl"];
            PhotosApiUrl= configuration["photosApiUrl"];
            UsersKey = configuration["usersKey"];
            PhotosKey = configuration["photosKey"];
            AlbumsKey = configuration["albumKey"];


        }
    }

 
}
