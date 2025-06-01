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

        [HttpPut("update-settings")]
        public IActionResult UpdateAllSettings([FromBody] ExtraHourSettingsDto settings)
        {
            try
            {
                _settingService.UpdateAsync(settings.Setting);

                foreach (var TypeHourName in settings.ExtraHourTypes)
                {
                    _extraHourTypeService.UpdateAsync(TypeHourName.Id.ToString(), TypeHourName);
                }

                return Ok(new { mensaje = "Configuraciones actualizadas correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar configuraciones: {ex.Message}");
            }
        }
    }
}