using Microsoft.AspNetCore.Mvc;
using ExtraHours.Core.Dto;
using ExtraHours.Core.Services;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroHorasController : ControllerBase
    {
        private readonly IRegistroHoraService _registroHoraService;

        public RegistroHorasController(IRegistroHoraService registroHoraService)
        {
            _registroHoraService = registroHoraService;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarHora([FromBody] CreateExtraHourDto dto)
        {
            await _registroHoraService.RegistrarHoraAsync(dto);
            return Ok();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> ObtenerHorasPorUsuario(int userId)
        {
            var horas = await _registroHoraService.ObtenerHorasPorUsuarioAsync(userId);
            return Ok(horas);
        }
    }
}
