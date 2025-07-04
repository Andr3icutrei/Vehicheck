﻿using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<CarModelDto>> AddCarModelAsync([FromBody] AddCarModelRequest payload)
        {
            try
            {
                var result = await _service.AddCarModelAsync(payload);
                return Ok(result);
            }
            catch (EntityNotFoundException)
            {
                throw;
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
        public async Task<ActionResult<CarModelDto>> GetCarModelByIdAsync(int id)
        {
            var result = await _service.GetCarModelAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CarModelDto>>> GetCarModelsAsync()
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
                return Ok($"Car model with id {id} deleted successfully");
            }
            catch (EntityNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting car model with id {CarModelId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CarModelDto>> PatchCarModelAsync([FromBody] PatchCarModelRequest payload)
        {
            try
            {
                var result = await _service.PatchCarModelAsync(payload);
                return StatusCode(StatusCodes.Status200OK,"Car model patched!");
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
        public async Task<ActionResult<PagedResponse<CarModelDto>>> GetCarModelsQueriedAsync(
            [FromQuery] CarModelQueryRequestDto payload)
        {
            try
            {
                var result = await _service.GetCarModelsQueryiedAsync(payload);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}