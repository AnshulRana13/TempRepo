using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentDemo.API.Models.Photos;
using AssignmentDemo.Entities.API.PhotoDetails;
using AutoMapper;

namespace AssignmentDemo.API.Profiles
{
    public class PhotosProfile : Profile
    {
        public PhotosProfile()
        {
            CreateMap<Photo, PhotoDto>();
        }
    }
}
