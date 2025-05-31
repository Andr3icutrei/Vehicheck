using Microsoft.AspNetCore.Mvc;
using Vehicheck.Core.Dtos.Requests;
using Vehicheck.Core.Dtos.Responses.Get;
using Vehicheck.Core.Services.Interfaces;
using Vehicheck.Database.Entities;

namespace Vehicheck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelsController : ControllerBase
    {
        private readonly ICarModelService _service;
        private readonly ILogger<CarModelsController> _logger;

        public CarModelsController(ICarModelService service, ILogger<CarModelsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarModel>> AddCarModelAsync([FromBody] AddCarModelRequest payload)
        {
            try
            {
                var result = await _service.AddCarModelAsync(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding car model");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetCarModelDto>> GetCarModelByIdAsync(int id)
        {
            try
            {
                var result = await _service.GetCarModelAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving car model with id {CarModelId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<GetCarModelDto>>> GetCarModelsAsync()
        {
            try
            {
                var result = await _service.GetCarModelsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving car models");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteCarModelAsync(int id)
        {
            try
            {
                var success = await _service.DeleteCarModelAsync(id);
                if (!success)
                {
                    return NotFound($"Car model with id {id} not found");
                }
                return Ok($"Car model with id {id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car model with id {CarModelId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}