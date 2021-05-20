using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentDemo.API.Models.Photos;
using AssignmentDemo.Entities.API.Common;
using AssignmentDemo.Entities.API.PhotoDetails;
using AssignmentDemo.Provider.PhotoRequest;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDemo.API.Controllers
{
    /// <summary>
    /// Photos Controller
    /// </summary>
    ///   [ApiVersion("1")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRequestHandler _photoRequestHandler;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="photoRequestHandler"></param>
        /// <param name="mapper"></param>
        public PhotosController(IPhotoRequestHandler photoRequestHandler,IMapper mapper)
        {
           _photoRequestHandler = photoRequestHandler;
            _mapper = mapper;
        }
        /// <summary>
        /// Get Photos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType,AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Photo>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<List<PhotoDto>>> GetPhotos()
        {
            var photos = await _photoRequestHandler.GetPhotos();
            return Ok(_mapper.Map<List<PhotoDto>>(photos));
        }
        /// <summary>
        /// Get Photo By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Id", Name = "GetPhoto")]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Photo>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<PhotoDto>> GetPhoto(int id)
        {
            var photo = await _photoRequestHandler.GetPhoto(id);
            if(photo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PhotoDto>(photo));
        }

        /// <summary>
        /// Get Allow Opertions fot the endpoint
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [MapToApiVersion("1")]   
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        public IActionResult GetPhotosOptions()
        {
            Response.Headers.Add("Allow","GET,OPTIONS");
            return Ok();
        }
    }
}
