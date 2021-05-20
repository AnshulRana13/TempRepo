using AssignmentDemo.Entities.API.AppConfig;
using AssignmentDemo.Entities.API.UserDetails;
using AssignmentDemo.Provider.Cache;
using AssignmentDemo.Provider.WebClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.UserRequest
{
    public class UserRequestHandler : IUserRequestHandler
    {
        private readonly IWebRequestHandler<User> _webRequestHandler;
        private readonly ICacheManager _cacheManager;
        private string URL { get; }
        private string userKey { get; }


        public UserRequestHandler(IWebRequestHandler<User> webRequestHandler, ICacheManager cacheManager)
        {
            _webRequestHandler = webRequestHandler;
            _cacheManager = cacheManager;
            URL = AppSettings.UsersApiUrl;
            userKey = AppSettings.UsersKey;
        }

        public async Task<User> GetUser(int id)
        {
            User user = null;
            if (_cacheManager.CheckIfKeyExists(userKey))
            {
                user = GetUsersFromCache(userKey).FirstOrDefault(x => x.id.Equals(id));
            }
            else
            {
               var tempres = await GetUserFromAPI();
                PutUsersInCache(tempres, userKey);
               user = tempres.FirstOrDefault(x => x.id.Equals(id)); 
            }

            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            if (_cacheManager.CheckIfKeyExists(userKey))
            {
                return GetUsersFromCache(userKey);
            }
            var res = await GetUserFromAPI();
            if (res.Count > 0)
            {
                PutUsersInCache(res, userKey);
            }

            return res;
        }

        private async Task<List<User>> GetUserFromAPI()
        {
            return await _webRequestHandler.GetDataByAll(URL);
        }

        private List<User> GetUsersFromCache(string key)
        {
            return _cacheManager.GetAll<User>(key);
        }

        private void PutUsersInCache(List<User> data, string key)
        {
            _cacheManager.PutAll<User>(key, data);
        }

 
    }
}
