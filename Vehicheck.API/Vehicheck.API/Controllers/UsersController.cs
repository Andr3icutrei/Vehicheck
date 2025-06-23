using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Microsoft.AspNetCore.Http;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Infrastructure.Exceptions;
using Vehicheck.Core.Dtos.Responses.Get.Querying;

namespace Vehicheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService service, ILogger<UsersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("add-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> AddUserAsync([FromBody] AddUserRequest payload)
        {
            try
            {
                var result = await _service.AddUserAsync(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id)
        { 
            var result = await _service.GetUserAsync(id);
            return Ok(result);
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            try
            {
                var result = await _service.GetUsersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            try
            {
                var success = await _service.DeleteUserAsync(id);
                return Ok($"User with id {id} deleted successfully");
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with id {UserId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserDto>> PatchCarModelAsync([FromBody] PatchUserRequest payload)
        {
            try
            {
                var result = await _service.PatchUserAsync(payload);
                return StatusCode(StatusCodes.Status200OK, "User patched!");
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error from the database");
            }
        }

        [HttpGet("queryied")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResponse<UserDto>>> GetUsersQueriedAsync(
            [FromQuery] UserQueryRequestDto payload)
        {
            try
            {
                var result = await _service.GetUsersQueryiedAsync(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
