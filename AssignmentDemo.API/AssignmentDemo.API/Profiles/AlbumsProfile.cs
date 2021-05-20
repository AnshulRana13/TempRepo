using AssignmentDemo.API.Models.Albums;
using AssignmentDemo.Entities.API.AlbumDetails;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentDemo.API.Profiles
{
    public class AlbumsProfile : Profile
    {
        public AlbumsProfile()
        {
            CreateMap<Album,AlbumDto>();
            CreateMap<AlbumDto, Album>();
            CreateMap<AlbumCreationDto, Album>();
            CreateMap<AlbumUpdateDto, Album>();
        }
    }
}
