using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentDemo.API.Models.Albums;
using AssignmentDemo.Entities.API.AlbumDetails;
using AssignmentDemo.Entities.API.Common;
using AssignmentDemo.Provider.AlbumRequest;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDemo.API.Controllers
{
    /// <summary>
    /// Albums Controller
    /// </summary>
    [ApiVersion("1")]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/albums")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {

        private readonly IAlbumRequestHandler _albumRequestHandler;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="albumRequestHandler"></param>
        /// <param name="mapper"></param>
        public AlbumsController(IAlbumRequestHandler albumRequestHandler, IMapper mapper)
        {
            _albumRequestHandler = albumRequestHandler;
            _mapper = mapper;
        }
        /// <summary>
        /// Get Albums
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Album>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<List<AlbumDto>>> GetAlbums()
        {
            var albums = await _albumRequestHandler.GetAlbums();
            return Ok(_mapper.Map<List<AlbumDto>>(albums));
        }
        /// <summary>
        /// Get Album by album Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Id", Name = "GetAlbumByUserId")]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Album>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<AlbumDto>> GetAlbumByUserId(int userId)
        {
            var album = await _albumRequestHandler.GetAlbum(userId);
            if (album == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<AlbumDto>(album));
        }


        /// <summary>
        /// Get Album by userId
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpGet("albumId", Name = "GetAlbumsGroupBy")]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Album>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<List<AlbumDto>>> GetAlbumsGroupBy(int albumId)
        {
            var albums = await _albumRequestHandler.GetAlbumByGroup(albumId);
            if(albums == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<AlbumDto>>(albums));
        }
        /// <summary>
        /// Add Album if User exist
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Album>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<AlbumDto>> AddAlbumForUser(AlbumCreationDto album)
        {

            bool isUserExist = await _albumRequestHandler.AlbumUserExist(album.userId);
            if (!isUserExist)
            {
                return NotFound();
            }
            var albumEntity = _mapper.Map<Album>(album);
            _albumRequestHandler.AddAlbum(albumEntity);

            var albumToReturn = _mapper.Map<AlbumDto>(albumEntity);

            return CreatedAtRoute("GetAlbumByUserId",
              new { userId = albumToReturn.userId, id = albumToReturn.id, },
              albumToReturn);

        }
        /// <summary>
        /// Update Title For User and Album
        /// </summary>
        /// <param name="album"></param>
        /// <returns></returns>

        [HttpPut]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Album>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<AlbumDto>> UpdateAlbumForUser(AlbumUpdateDto album)
        {

            bool isUserAndAlbumExist = await _albumRequestHandler.AlbumAndUserExist(album.userId,album.id);
            if (!isUserAndAlbumExist)
            {
                return NotFound();
            }
            var albumEntity = _mapper.Map<Album>(album);
            _albumRequestHandler.UpdateAlbumForUser(albumEntity);

            return NoContent();
        }




        /// <summary>
        /// Get Allow Opertions for the endpoint
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [MapToApiVersion("1")]
        [MapToApiVersion("2")]
        [Produces(AppConstant.JsonContentType,AppConstant.XmlContentType)]
        public IActionResult GetAlbumOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS,POST");
            return Ok();
        }
    }
}
