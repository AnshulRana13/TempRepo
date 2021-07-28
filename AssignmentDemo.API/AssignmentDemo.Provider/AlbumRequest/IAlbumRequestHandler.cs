using AssignmentDemo.Entities.API.AlbumDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.AlbumRequest
{
    public interface IAlbumRequestHandler
    {
        Task<List<Album>> GetAlbums();
        Task<Album> GetAlbum(int id);
        Task<List<Album>> GetAlbumByGroup(int albumId);

        Task<bool> AlbumUserExist(int userId);
        Task AddAlbum(Album album);

        Task UpdateAlbumForUser(Album album);

        Task<bool> AlbumAndUserExist(int userId, int albumId);
    }
}
