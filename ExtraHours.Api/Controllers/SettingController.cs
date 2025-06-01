using ExtraHours.Core.dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [Route("api/setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        readonly ISettingService _settingService;
        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost("createSetting")]
        public async Task<IActionResult> CreateSetting([FromBody] SettingDto settingDto)
        {
            await _settingService.AddAsync(settingDto);
            return Ok(settingDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSetting()
        {
            var result = await _settingService.GetAllAsync();
            return Ok(result);
        }
    }
}