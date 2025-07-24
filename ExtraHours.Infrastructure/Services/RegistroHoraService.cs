using Microsoft.EntityFrameworkCore;
using ExtraHours.Core.dto;
using ExtraHours.Core.Models;
using ExtraHours.Core.Services;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Services
{
    public class RegistroHoraService : IRegistroHoraService
    {
        private readonly AppDbContext _context;

        public RegistroHoraService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarHoraAsync(CreateExtraHourDto dto)
        {
            var entidad = new RegistroHora
            {
                UserId = dto.UserId,
                CantidadHoras = dto.CantidadHoras,
                TipoHora = dto.TipoHora,
                Fecha = dto.Fecha.ToString("yyyy-MM-dd")
            };

            _context.RegistroHoras.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RegistroHora>> ObtenerHorasPorUsuarioAsync(int userId)
        {
            return await _context.RegistroHoras
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExtraHourSummaryDto>> ObtenerHorasConUsuarioAsync()
        {
            return await _context.RegistroHoras
                .Include(r => r.User)
                .Select(r => new ExtraHourSummaryDto
                {
                    Id = r.Id,
                    UserName = r.User != null ? r.User.Name : "Sin nombre",
                    Code = r.Id.ToString(),
                    Fecha = r.Fecha,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    CantidadHoras = r.CantidadHoras,
                    Estado = "Pendiente"
                })
                .ToListAsync();
        }
    }
}




