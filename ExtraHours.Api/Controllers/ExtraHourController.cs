using ExtraHours.Core.dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/hour")]
    public class ExtraHourController : ControllerBase
    {
        readonly IExtraHourService _extraHourService;
        public ExtraHourController(IExtraHourService extraHourService)
        {
            _extraHourService = extraHourService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateExtraHour([FromBody] ExtraHourDto extraHourDto)
        {
            await _extraHourService.AddAsync(extraHourDto);
            return Ok(extraHourDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExtraHours()
        {
            var result = await _extraHourService.GetAllAsync();
            return Ok(result);
        }

    }

}
