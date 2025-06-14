using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;

namespace Vehicheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentManufacturersController : ControllerBase
    {
        private readonly IComponentManufacturerService _service;
        private readonly ILogger<ComponentManufacturersController> _logger;

        public ComponentManufacturersController(IComponentManufacturerService service, ILogger<ComponentManufacturersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ComponentManufacturer>> AddComponentManufacturerAsync([FromBody] AddComponentManufacturerRequest payload)
        {
            try
            {
                var result = await _service.AddComponentManufactureAsync(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding component manufacturer");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetComponentManufacturerDto>> GetComponentManufacturerByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetComponentManufacturerAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving component manufacturer with id {ComponentManufacturerId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GetComponentManufacturerDto>>> GetComponentManufacturersAsync()
        {
            try
            {
                var result = await _service.GetComponentManufacturesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving component manufacturers");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteComponentManufacturerAsync(int id)
        {
            try
            {
                var success = await _service.DeleteComponentManufacturerAsync(id);
                if (!success)
                {
                    return NotFound($"Component manufacturer with id {id} not found");
                }
                return Ok($"Component manufacturer with id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting component manufacturer with id {ComponentManufacturerId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
