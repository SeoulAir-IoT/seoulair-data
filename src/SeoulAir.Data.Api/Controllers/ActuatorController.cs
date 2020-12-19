using Microsoft.AspNetCore.Mvc;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace SeoulAir.Data.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActuatorController : ControllerBase
    {
        private readonly IMqttListenerService<DataRecordDto> _mqttService;
        public ActuatorController(IMqttListenerService<DataRecordDto> mqttService)
        {
            _mqttService = mqttService;
        }

        [HttpGet("IsOn")]
        public IActionResult IsOn()
        {
            return Ok(_mqttService.IsConnected());
        } 

        [HttpPost("TurnOn")]
        public async Task<IActionResult> TurnOn()
        {
            await _mqttService.OpenConnection();
            await _mqttService.SubscribeToTopic();
            return Ok();
        }

        [HttpPost("TurnOff")]
        public async Task<IActionResult> TurnOff()
        {
            await _mqttService.CloseConnection();
            return Ok();
        }
    }
}
