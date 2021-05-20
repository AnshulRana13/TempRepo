using AssignmentDemo.Entities.API.PhotoDetails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentDemo.Provider.PhotoRequest
{
    public interface IPhotoRequestHandler
    {
        Task<List<Photo>> GetPhotos();
        Task<Photo> GetPhoto(int id);
    }
}
