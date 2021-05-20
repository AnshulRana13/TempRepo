using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentDemo.API.Models.Users;
using AssignmentDemo.Entities.API.Common;
using AssignmentDemo.Entities.API.UserDetails;
using AssignmentDemo.Provider.UserRequest;
using AssignmentDemo.Provider.WebClient;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentDemo.API.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    /// [ApiVersion("1")]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRequestHandler _userRequestHandler;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userRequestHandler"></param>
        /// <param name="mapper"></param>
        public UsersController(IUserRequestHandler userRequestHandler, IMapper mapper)
        {
            _userRequestHandler = userRequestHandler;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Users
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<UserDto>))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userRequestHandler.GetUsers();          
            return Ok(_mapper.Map<List<UserDto>>(users));
        }
        /// <summary>
        /// Get User By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Id", Name = "GetUser")]
        [MapToApiVersion("1")]
        [Produces(AppConstant.JsonContentType, AppConstant.XmlContentType)]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(UserDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorResult))]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorResult))]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = await _userRequestHandler.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserDto>(user));
        }


        /// <summary>
        /// Get Allow Opertions for the endpoint
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        [MapToApiVersion("2")]
        [Produces(AppConstant.JsonContentType)]
        public IActionResult GetUsersOptions()
        {
            Response.Headers.Add("Allow", "GET,OPTIONS");
            return Ok();
        }

    }
}
