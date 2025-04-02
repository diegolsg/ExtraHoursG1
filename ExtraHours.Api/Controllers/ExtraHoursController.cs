using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExtraHours.Infrastructure.Data; 
using ExtraHours.Core.Models;
using System.Threading.Tasks;

namespace ExtraHours.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorasExtraController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public HorasExtraController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("registrar-horas-extra")]
        public async Task<IActionResult> RegistrarHorasExtra([FromBody] ExtraHour extraHours)
        {
            var empleado = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Code == extraHours.Code);

            if (empleado == null)
            {
                return NotFound("Empleado no encontrado.");
            }

            // Si el código existe, proceder con el registro de las horas extras
            var horasExtraRegistradas = new ExtraHour
            {
                Code = extraHours.Code,
                date = extraHours.date,
                StartTime = extraHours.StartTime,
                EndTime = extraHours.EndTime,
            };

            _dbContext.ExtraHours.Add(horasExtraRegistradas);
            await _dbContext.SaveChangesAsync();

            return Ok("Horas extras registradas exitosamente.");
        }
    }
}