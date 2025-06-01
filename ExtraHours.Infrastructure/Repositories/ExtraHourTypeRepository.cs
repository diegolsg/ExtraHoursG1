using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class ExtraHourTypeRepository : IExtraHourTypeRepository
    {
        private readonly AppDbContext _context;

        public ExtraHourTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ExtraHourType entity)
        {
            await _context.ExtraHourTypes.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExtraHourType>> GetAllAsync()
        {
            return await _context.ExtraHourTypes
                .ToListAsync();
        }

        public async Task<ExtraHourType> GetByTypeHourNameAsync(string typeHourName)
        {
            return await _context.ExtraHourTypes
            .FirstOrDefaultAsync(x => x.TypeHourName == typeHourName);
        }

        public Task UpdateAsync(ExtraHourType entity)
        {
            _context.ExtraHourTypes.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
