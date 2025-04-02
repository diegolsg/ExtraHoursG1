using ExtraHours.Core.Repositories;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class ExtraHourRepository : IExtraHourRepository
    {
        private readonly AppDbContext _context;

        public ExtraHourRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExtraHour>> GetAllAsync()
        {
            return await _context.ExtraHours.ToListAsync();
        }

        public async Task<ExtraHour> GetByIdAsync(int id)
        {
            return await _context.ExtraHours.FindAsync(id) ?? throw new KeyNotFoundException($"ExtraHour with ID {id} not found.");
        }

        public async Task AddAsync(ExtraHour extraHour)
        {
            await _context.ExtraHours.AddAsync(extraHour);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ExtraHour extraHour)
        {
            _context.ExtraHours.Update(extraHour);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var extraHour = await _context.ExtraHours.FindAsync(id);
            if (extraHour != null)
            {
                _context.ExtraHours.Remove(extraHour);
                await _context.SaveChangesAsync();
            }
        }
    }
}
