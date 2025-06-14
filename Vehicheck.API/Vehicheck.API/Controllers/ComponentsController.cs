using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;

namespace Vehicheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Component>> AddComponentAsync([FromBody] AddComponentRequest payload)
        {
            try
            {
                var result = await _service.AddComponentAsync(payload);
                return Ok(result);
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
        public async Task<ActionResult<GetComponentDto>> GetComponentByIdAsync(int id)
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
        public async Task<ActionResult<List<GetComponentDto>>> GetComponentsAsync()
        {
            try
            {
                var result = await _service.GetComponentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving components");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
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
                if (!success)
                {
                    return NotFound($"Component with id {id} not found");
                }
                return Ok($"Component with id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting component with id {ComponentId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
