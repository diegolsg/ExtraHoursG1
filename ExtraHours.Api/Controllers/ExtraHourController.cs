using Azure;
using ExtraHours.Core.dto;
using ExtraHours.Core.Interfeces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/hour")]
    public class ExtraHourController : ControllerBase
    {
        readonly IService<ExtraHourDto> _extraHourService;
        public ExtraHourController(IService<ExtraHourDto> extraHourService)
        {
            _extraHourService = extraHourService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RegisterHourDto registerHourDto)
        {
            var result = await _extraHourService.CreateAsync(registerHourDto);
            return Ok(result);
        }
    }
}
