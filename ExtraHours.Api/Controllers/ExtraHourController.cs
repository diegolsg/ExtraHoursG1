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

        [HttpGet("dto")]
        public async Task<IActionResult> GetAllExtraHoursWithDto()
        {
            var result = await _extraHourService.GetAllWithDtoAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExtraHourById(int id)
        {
            var extraHour = await _extraHourService.GetByIdAsync(id);
            if (extraHour == null) return NotFound();
            return Ok(extraHour);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> HourStatus(int id, [FromBody] HourStatusDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Status))
            {
                return BadRequest("Invalid status data.");
            }

            try
            {
                await _extraHourService.HourStatus(id, dto.Status);
                return Ok(new { message = "Status updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
