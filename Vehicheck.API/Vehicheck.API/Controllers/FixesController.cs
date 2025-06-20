using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Dtos.Responses.Get.Querying;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Infrastructure.Exceptions;

namespace Vehicheck.API.Controllers
{
    namespace Vehicheck.API.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class FixesController : ControllerBase
        {
            private readonly IFixService _service;
            private readonly ILogger<FixesController> _logger;

            public FixesController(IFixService service, ILogger<FixesController> logger)
            {
                _service = service;
                _logger = logger;
            }

            [HttpPost]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> AddFixAsync([FromBody] AddFixRequest payload)
            {
                try
                {
                    var result = await _service.AddFixAsync(payload);
                    return Ok("Fix added!");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error adding fix");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
                }
            }

            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<FixDto>> GetFixByIdAsync(int id)
            {
                var result = await _service.GetFixAsync(id);
                return Ok(result);   
            }

            [HttpGet]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<List<FixDto>>> GetFixesAsync()
            {
                try
                {
                    var result = await _service.GetFixesAsync();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving fixes");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
                }
            }

            [HttpDelete("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult> DeleteFixAsync(int id)
            {
                try
                {
                    var success = await _service.DeleteFixAsync(id);
                    return Ok($"Fix with id {id} deleted successfully");
                }
                catch (EntityNotFoundException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting fix with id {FixId}", id);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
                }
            }

            [HttpPatch]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public async Task<ActionResult<FixDto>> PatchFixAsync([FromBody] PatchFixRequest payload)
            {
                try
                {
                    var result = await _service.PatchFixAsync(payload);
                    return StatusCode(StatusCodes.Status200OK, "Fix patched!");
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
            public async Task<ActionResult<PagedResponse<FixDto>>> GetFixesQueriedAsync(
                [FromQuery] FixQueryRequestDto payload)
            {
                try
                {
                    var result = await _service.GetFixesQueryiedAsync(payload);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
                }
            }
        }
    }
}
