using ExtraHours.Core.dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/extraHourSettings")]
    public class ExtraHourSettingController : ControllerBase
    {
        private readonly ISettingService _settingService;
        private readonly IExtraHourTypeService _extraHourTypeService;

        public ExtraHourSettingController(ISettingService settingService, IExtraHourTypeService extraHourTypeService)
        {
            _settingService = settingService;
            _extraHourTypeService = extraHourTypeService;
        }

        [HttpGet("get-settings")]
        public async Task<IActionResult> GetAllSettings()
        {
            var settings = await _settingService.GetAllAsync();
            var extraHourTypes = await _extraHourTypeService.GetAllAsync();

            return Ok(new
            {
                setting = settings,
                extraHourType = extraHourTypes,
                extraHourTypeSelected = extraHourTypes.Select(t => new
                {
                    t.TypeHourName,
                })
            });
        }

        [HttpPut("update-settings")]
        public async Task<IActionResult> UpdateAllSettings([FromBody] ExtraHourSettingsDto extraHourSettingsDto)
        {
            try
            {
                await _settingService.UpdateAsync(extraHourSettingsDto.Setting);

                foreach (var typeHourName in extraHourSettingsDto.ExtraHourTypes)
                {
                    await _extraHourTypeService.UpdateAsync(typeHourName.Id.ToString(), typeHourName);
                }

                return Ok(new
                {
                    message = "Configuraciones actualizadas correctamente",
                    settings = extraHourSettingsDto.Setting,
                    extraHourTypes = extraHourSettingsDto.ExtraHourTypes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar configuraciones: {ex.Message}");
            }
        }

    }
}