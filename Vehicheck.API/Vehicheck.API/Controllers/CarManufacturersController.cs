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
    public class CarManufacturersController : ControllerBase
    {
        private readonly ICarManufacturerService _service;
        private readonly ILogger<CarManufacturersController> _logger;

        public CarManufacturersController(ICarManufacturerService service, ILogger<CarManufacturersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarManufacturer>> AddCarManufacturerAsync([FromBody] AddCarManufacturerRequest payload)
        {
            try
            {
                var result = await _service.AddCarManufacturerAsync(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarManufacturerDto>> GetCarManufacturerByIdAsync(int id)
        {
            var result = await _service.GetCarManufacturerAsync(id);
            return Ok(result);
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CarManufacturerDto>>> GetCarManufacturersAsync()
        {
            try
            {
                var result = await _service.GetAllCarManufacturersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("queryied")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedResponse<CarManufacturerDto>>> GetCarManufacturersQueriedAsync(
            [FromQuery] CarManufacturerQueryRequestDto payload)
        {
            try
            {
                var result = await _service.GetCarManufacturersQueriedAsync(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCarManufacturerAsync(int id)
        {
            try
            {
                var success = await _service.DeleteCarManufacturerAsync(id);
                return StatusCode(StatusCodes.Status200OK,"Car manufacturer with id {id} deleted successfully");
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car manufacturer with id {CarManufacturerId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarManufacturer>> PatchCarManufacturerAsync([FromBody] PatchCarManufacturerRequest payload)
        {
            try
            {
                var result = await _service.PatchCarManufacturerAsync(payload);
                return StatusCode(StatusCodes.Status200OK, "Car manufacturer patched!");
            }
            //catch(EntityNotFoundException)
            //{
            //    throw;
            //}
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error patching data from the database");
            }
        }
    }
}
