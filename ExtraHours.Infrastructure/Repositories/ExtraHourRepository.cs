using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Repositories
{
    public class ExtraHourRepository : IExtraHourRepository
    {
        readonly AppDbContext _context;
        public ExtraHourRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ExtraHour entity)
        {
            await _context.ExtraHours.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            ExtraHour extraHour = await GetByIdAsync(id);
            _context.ExtraHours.Remove(extraHour);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExtraHour>> GetAllAsync()
        {
            return await Task.FromResult(_context.ExtraHours.ToList());
        }

        public async Task<ExtraHour> GetByIdAsync(int id)
        {
            var extraHour = await _context.ExtraHours.FindAsync(id);
            if (extraHour == null)
            {
                throw new KeyNotFoundException($"ExtraHour with ID {id} not found");
            }
            return extraHour;
        }

        public Task UpdateAsync(ExtraHour entity)
        {
            throw new NotImplementedException();
        }


    }
}
