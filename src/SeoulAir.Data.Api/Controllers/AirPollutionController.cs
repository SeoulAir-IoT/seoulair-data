using Microsoft.AspNetCore.Mvc;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Services;
using System.Threading.Tasks;

namespace SeoulAir.Data.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirPollutionController : ControllerBase
    {
        private readonly IAirPollutionService _service;

        public AirPollutionController(IAirPollutionService service) : base()
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(DataRecordDto toCreate)
        {
            await _service.AddAsync(toCreate);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] Paginator paginator)
        {
            return Ok(await _service.GetPaginated(paginator));
        }
    }
}
