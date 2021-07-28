using AssignmentDemo.Entities.API.AlbumDetails;
using AssignmentDemo.Entities.API.AppConfig;
using AssignmentDemo.Provider.Cache;
using AssignmentDemo.Provider.WebClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.AlbumRequest
{
    public class AlbumRequestHandler : IAlbumRequestHandler
    {
        private readonly IWebRequestHandler<Album> _webRequestHandler;
        private readonly ICacheManager _cacheManager;
        private string URL { get; }
        private string albumKey { get; }

        public AlbumRequestHandler(IWebRequestHandler<Album> webRequestHandler, ICacheManager cacheManager)
        {
            _webRequestHandler = webRequestHandler;
            _cacheManager = cacheManager;
            URL = AppSettings.AlbumsApiUrl;
            albumKey = AppSettings.AlbumsKey;
        }



        public async Task<Album> GetAlbum(int userId)
        {
            Album album = null;
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                album = GetAlbumsFromCache(albumKey).FirstOrDefault(x => x.userId.Equals(userId));
            }
            else
            {
                var tempres = await GetAlbumFromAPI();
                PutAlbumsInCache(tempres, albumKey);
                album = tempres.FirstOrDefault(x => x.id.Equals(userId));
            }

            return album;
        }

        public async Task<List<Album>> GetAlbums()
        {
         
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                return GetAlbumsFromCache(albumKey);
            }
            var res = await GetAlbumFromAPI();
            if (res.Count > 0)
            {
                PutAlbumsInCache(res, albumKey);
            }                          
            
            return res;
        }

        private async Task<List<Album>> GetAlbumFromAPI()
        {
            return await _webRequestHandler.GetDataByAll(URL);
        }
        private List<Album> GetAlbumsFromCache(string key)
        {
            return _cacheManager.GetAll<Album>(key); 
        }

        private void PutAlbumsInCache(List<Album> data, string key)
        {
            _cacheManager.PutAll<Album>(key, data);
        }

        public async Task<List<Album>> GetAlbumByGroup(int userId)
        {
            List<Album> result = null;
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                result = GetAlbumsFromCache(albumKey);
               
            }
            else
            {
                result = await GetAlbumFromAPI();
                if (result != null && result.Count > 0)
                {
                    PutAlbumsInCache(result, albumKey);
                }
            }     
            result = GetGroupByUserId(result, userId);
            return result;

        }

        private List<Album> GetGroupByUserId(List<Album> lst , int userId)
        {
          return lst.GroupBy(x => x.userId == userId).FirstOrDefault(res => res.Key == true).ToList<Album>();
        }

        public async Task<bool> AlbumUserExist(int userId)
        {
            bool isUserExist;
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                isUserExist =  GetAlbumsFromCache(albumKey).Any(x=>x.userId == userId);
                return isUserExist;
            }
            else
            {
                var tempres = await GetAlbumFromAPI();
                PutAlbumsInCache(tempres, albumKey);
                isUserExist = tempres.Any(x => x.userId.Equals(userId));
            }
            return isUserExist;

        }

        public async Task AddAlbum(Album album)
        {
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                var res = GetAlbumsFromCache(albumKey);
                res.Add(album);

            }
            else
            {
                var tempres = await GetAlbumFromAPI();
                tempres.Add(album);
                PutAlbumsInCache(tempres, albumKey);
            }
        }

        public async Task UpdateAlbumForUser(Album album)
        {
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                var res = GetAlbumsFromCache(albumKey);
                UpdateAlbum(res,album);

            }
            else
            {
                var tempres = await GetAlbumFromAPI();
                UpdateAlbum(tempres,album);
                PutAlbumsInCache(tempres, albumKey);
            }
        }

        private void UpdateAlbum(List<Album> lst ,Album album)
        {
            lst.ForEach(item =>
            {
                if (item.userId == album.userId && item.id == album.id)
                {
                    item.userId = album.userId;
                    item.id = album.id;

                }
            });
        }

        public async Task<bool> AlbumAndUserExist(int userId, int albumId)
        {
            bool isExist;
            if (_cacheManager.CheckIfKeyExists(albumKey))
            {
                isExist = GetAlbumsFromCache(albumKey).Any(x => x.userId == userId && x.id == albumId);
                return isExist;

            }
            else
            {
                var tempres = await GetAlbumFromAPI();
                PutAlbumsInCache(tempres, albumKey);
                isExist = tempres.Any(x => x.userId.Equals(userId) && x.id.Equals(albumId));
            }
            return isExist;
        }
    }
}
