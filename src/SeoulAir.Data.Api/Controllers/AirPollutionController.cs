﻿using System;
using Microsoft.AspNetCore.Mvc;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SeoulAir.Data.Api.Controllers
{
    /// <summary>
    /// Responsible for all CRUD operations on data that is received from sensors.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AirPollutionController : ControllerBase
    {
        private readonly IAirPollutionService _service;
        private readonly IAnalyticsService _analyticsService;

        public AirPollutionController(IAirPollutionService service, IAnalyticsService analyticsService)
        {
            _service = service;
            _analyticsService = analyticsService;
        }

        /// <summary>
        /// Read (CRUD) operation. Gets the DataRecord from MongoDb Database. Matches the record by Id.  
        /// </summary>
        /// <param name="id">Unique string that represents the entity in database.</param>
        /// <response code="200">Operation completed successfully, requested resource found and returned</response>
        /// <response code="204">Operation completed successfully, requested resource does not exist</response>
        [ProducesResponseType(typeof(DataRecordDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        public async Task<ActionResult<DataRecordDto>> Get(string id)
        {
            var result = await _service.GetById(id);
            if (result == default)
                return NoContent();
            
            return Ok(result);
        }

        /// <summary>
        /// Create (CRUD) operation. Creates the DataRecord instance in MongoDb Database.
        /// </summary>
        /// <param name="dataRecord">Data record to be created.</param>
        /// <remarks>
        /// Id should not be passed. If it is passed it is going to be ignored.
        /// Id is generated by the system
        /// </remarks>
        /// <response code="201">Operation completed successfully,
        /// data record has been created with returned id.</response>
        [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<string>> Post(DataRecordDto dataRecord)
        {
            string createdId = await _service.AddAsync(dataRecord);
            Task.Run(() => _analyticsService.SendDataToAnalyticsService(dataRecord));
            
            return CreatedAtAction(nameof(Post), createdId);
        }

        /// <summary>
        /// Deletes (CRUD) operation. Deletes data record from the database.
        /// </summary>
        /// <param name="id">Unique string that represents the entity to be deleted.</param>
        /// <response code="204">Operation completed successfully, data record has been deleted from database</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            
            return NoContent();
        }

        /// <summary>
        /// Reads multiple entries from database but in paginated form.
        /// </summary>
        /// <param name="paginator">Parameters for generating page.</param>
        /// <response code="200">Operation completed successfully, one page returned</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("page")]
        public async Task<ActionResult<PaginatedResultDto<DataRecordDto>>> GetPaginated([FromQuery] Paginator paginator)
        {
            return Ok(await _service.GetPaginated(paginator));
        }
    }
}
