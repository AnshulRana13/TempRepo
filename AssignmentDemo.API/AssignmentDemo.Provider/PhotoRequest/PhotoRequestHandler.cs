using AssignmentDemo.Entities.API.AlbumDetails;
using AssignmentDemo.Entities.API.AppConfig;
using AssignmentDemo.Entities.API.PhotoDetails;
using AssignmentDemo.Provider.Cache;
using AssignmentDemo.Provider.WebClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.PhotoRequest
{
    public class PhotoRequestHandler : IPhotoRequestHandler
    {

        private readonly IWebRequestHandler<Photo> _webRequestHandler;
        private readonly ICacheManager _cacheManager;
        private string URL { get; }
        private string photoKey { get; }

        public PhotoRequestHandler(IWebRequestHandler<Photo> webRequestHandler, ICacheManager cacheManager)
        {
            _webRequestHandler = webRequestHandler;
            _cacheManager = cacheManager;
            URL = AppSettings.PhotosApiUrl;
            photoKey = AppSettings.PhotosKey;

        }

        public async Task<List<Photo>> GetPhotos()
        {
           
            if (_cacheManager.CheckIfKeyExists(photoKey))
            {
                return GetPhotosFromCache(photoKey);
            }
            var res = await _webRequestHandler.GetDataByAll(URL);
            if (res.Count > 0)
            {
                PutPhotosInCache(res, photoKey);
            }

            return res;
           
        }

        public async Task<Photo> GetPhoto(int id)
        {
            Photo photo = null;
            if (_cacheManager.CheckIfKeyExists(photoKey))
            {
                photo = GetPhotosFromCache(photoKey).FirstOrDefault(x => x.id.Equals(id));
            }
            else
            {
                var tempres = await GetPhotosFromAPI();
                PutPhotosInCache(tempres, photoKey);
                photo = tempres.FirstOrDefault(x => x.id.Equals(id));
            }

            return photo;
        }

        private async Task<List<Photo>> GetPhotosFromAPI()
        {
            return await _webRequestHandler.GetDataByAll(URL);
        }

        private List<Photo> GetPhotosFromCache(string key)
        {
            return _cacheManager.GetAll<Photo>(key);
        }

        private void PutPhotosInCache(List<Photo> data, string key)
        {
            _cacheManager.PutAll<Photo>(key, data);
        }
    }
}
