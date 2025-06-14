using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Dtos.Requests.Patch;
using Vehicheck.Core.Dtos.Requests.Post;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;
using Vehicheck.Infrastructure.Exceptions;

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
            var result = await _service.GetComponentManufacturerAsync(id);
            return Ok(result);
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
                return Ok($"Component manufacturer with id {id} deleted successfully");
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting component manufacturer with id {ComponentManufacturerId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetComponentManufacturerDto>> PatchComponentManufacturerAsync([FromBody] PatchComponentManufacturerRequest payload)
        {
            try
            {
                var result = await _service.PatchComponentManufacturerAsync(payload);
                return StatusCode(StatusCodes.Status200OK, "Component manufacturer patched!");
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
    }
}
