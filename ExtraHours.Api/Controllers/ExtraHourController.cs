using Microsoft.AspNetCore.Mvc;
using ExtraHours.Core.Interfaces.IServices;
using ExtraHours.Core.Dtos;
using ExtraHours.Core.Models;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExtraHourController : ControllerBase
    {
        private readonly IExtraHourService _service;

        public ExtraHourController(IExtraHourService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<ExtraHour>> GetAll() => await _service.GetAllAsync();

        [HttpGet("{id}")]
        public async Task<ExtraHour> Get(int id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExtraHourDto dto)
        {
            await _service.AddAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = dto.UserId }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExtraHourDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}