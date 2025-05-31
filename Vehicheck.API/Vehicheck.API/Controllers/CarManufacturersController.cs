using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;


namespace Vehicheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<Car>> GetCarByIdAsync(int id)
        {
            try
            {
                var result = _service.GetCarManufacturerAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Car>> GetCarsAsync()
        {
            try
            {
                var result = _service.GetAllCarManufacturersAsync();
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
        public async Task<ActionResult> DeleteCarManufacturerAsync(int id)
        {
            try
            {
                var success = await _service.DeleteCarManufacturerAsync(id);
                if (!success)
                {
                    return NotFound($"Car manufacturer with id {id} not found");
                }
                return Ok($"Car manufacturer with id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car manufacturer with id {CarManufacturerId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
