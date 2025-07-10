using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ExtraHours.Core.Repositories;


namespace ExtraHours.Infrastructure.Repositories

{
    public class RegistroHoraRepository : IRegistroHoraRepository
    {
        private readonly AppDbContext _context;

        public RegistroHoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RegistroHora registro)
        {
            _context.RegistroHoras.Add(registro);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RegistroHora>> GetByUserAsync(int userId)
        {
            return await _context.RegistroHoras
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync();
        }
    }
}
