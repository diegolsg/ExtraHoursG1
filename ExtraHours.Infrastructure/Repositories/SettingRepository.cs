using ExtraHours.Core.Interfeces.IRepositories;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class SettingsRepository : IRepository<Settings>
    {
        private readonly AppDbContext _context;

        public SettingsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Setting>> GetAll()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Setting> GetById(int id)
        {
            var setting = await _context.Settings.FindAsync(id);
            if (setting == null)
            {
                throw new KeyNotFoundException($"Setting with ID {id} not found");
            }
            return setting;
        }

        public async Task Create(Setting entity)
        {
            await _context.Settings.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Setting entity)
        {
            var existingSetting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == entity.Id);
            if (existingSetting == null)
            {
                throw new KeyNotFoundException($"Setting with ID {entity.Id} does not exist.");
            }

            existingSetting.Key = entity.Key;
            existingSetting.Value = entity.Value;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var setting = await GetById(id);
            _context.Settings.Remove(setting);
            await _context.SaveChangesAsync();
        }
    }
}
