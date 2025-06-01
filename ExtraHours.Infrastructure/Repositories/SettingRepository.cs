using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class SettingRepository: ISettingRepository
    {
        readonly AppDbContext _context;
        public SettingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Setting entity)
        {
            await _context.Settings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Setting>> GetAllAsync()
        {
            return await _context.Settings
                .ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                throw new KeyNotFoundException($"ExtraHour with ID {id} not found");
            }
            return setting;
        }

        public Task UpdateAsync(Setting entity)
        {
            _context.Settings.Update(entity);
            return _context.SaveChangesAsync();
        }
    }
}
