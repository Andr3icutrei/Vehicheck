using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComponentsController : ControllerBase
    {
        private readonly IComponentService _service;
        private readonly ILogger<ComponentsController> _logger;

        public ComponentsController(IComponentService service, ILogger<ComponentsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddComponentAsync([FromBody] AddComponentRequest payload)
        {
            try
            {
                var result = await _service.AddComponentAsync(payload);
                return Ok("Component added!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding component");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComponentDto>> GetComponentByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetComponentAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving component with id {ComponentId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<ComponentDto>>> GetComponentsAsync()
        {
            var result = await _service.GetComponentsAsync();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteComponentAsync(int id)
        {
            try
            {
                var success = await _service.DeleteComponentAsync(id);
                return Ok($"Component with id {id} deleted successfully");
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting component with id {ComponentId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComponentDto>> PatchComponentAsync([FromBody] PatchComponentRequest payload)
        {
            try
            {
                var result = await _service.PatchComponentAsync(payload);
                return StatusCode(StatusCodes.Status200OK, "Component patched!");
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
        public async Task<ActionResult<PagedResponse<ComponentDto>>> GetComponentQueriedAsync(
            [FromQuery] ComponentQueryRequestDto payload)
        {
            try
            {
                var result = await _service.GetComponentsQueriedAsync(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
