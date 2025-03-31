using ExtraHours.Core.dto;
using ExtraHours.Core.Interfeces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly IService<dtoSettings> _settingsService;

        public SettingsController(IService<dtoSettings> settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<dtoSettings>>> Get()
        {
            try
            {
                var settings = await _settingsService.GetAllUserAsync();
                return Ok(settings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var setting = await _settingsService.GetByIdUserAsync(id);
                return setting != null ? Ok(setting) : NotFound(new { message = "Setting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] dtoSettings setting)
        {
            try
            {
                var createdSetting = await _settingsService.CreateUserAsync(setting);
                return CreatedAtAction(nameof(Get), new { id = createdSetting.Id }, createdSetting);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating setting", details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] dtoSettings setting)
        {
            if (id != setting.Id)
                return BadRequest(new { message = "Setting ID mismatch" });

            try
            {
                await _settingsService.UpdateUserAsync(setting, id);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Setting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating setting", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _settingsService.DeleteUserAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Setting not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting setting", details = ex.Message });
            }
        }
    }
}
