using Microsoft.AspNetCore.Mvc;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SeoulAir.Data.Api.Controllers
{
    /// <summary>
    /// Acts as virtual actuator to microservice.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ActuatorController : ControllerBase
    {
        private readonly IMqttListenerService<DataRecordDto> _mqttService;
        public ActuatorController(IMqttListenerService<DataRecordDto> mqttService)
        {
            _mqttService = mqttService;
        }

        /// <summary>
        /// Checks if microservice is receiving data on on MQTT topic.
        /// </summary>
        /// <response code="200">Operation completed successfully and boolean result is returned</response>
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpGet("IsOn")]
        public ActionResult<bool> IsOn()
        {
            return Ok(_mqttService.IsConnected());
        } 

        /// <summary>
        /// Connects microservice to MQTT topic and starts receiving data. Data is then sent to Analytics service.
        /// </summary>
        /// <response code="204">Operation completed successfully. Microservice is now on.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("TurnOn")]
        public async Task<IActionResult> TurnOn()
        {
            await _mqttService.OpenConnection();
            await _mqttService.SubscribeToTopic();
            
            return NoContent();
        }

        /// <summary>
        /// Disconnects microservice from MQTT topic.
        /// After this action service is going to stop receiving and sending data.
        /// </summary>
        /// <response code="204">Operation completed successfully. Microservice is now on.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPost("TurnOff")]
        public async Task<IActionResult> TurnOff()
        {
            await _mqttService.CloseConnection();
            
            return NoContent();
        }
    }
}
