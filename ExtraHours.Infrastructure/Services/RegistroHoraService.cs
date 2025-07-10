using Microsoft.EntityFrameworkCore;
using ExtraHours.Core.Dto;
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
                Fecha = dto.Fecha
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
    }
}


