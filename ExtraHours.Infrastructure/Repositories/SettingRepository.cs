using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Repositories
{
    public class SettingRepository: ISettingRepository
    {
        readonly AppDbContext _context;
        public SettingRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Setting entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setting>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Setting> GetByIdAsync(int id)
        {
            var setting = _context.Settings.Find(id);
            if (setting == null)
            {
                throw new KeyNotFoundException($"Setting with ID {id} not found");
            }
            return Task.FromResult(setting);
        }

        public Task UpdateAsync(Setting entity)
        {
            throw new NotImplementedException();
        }
    }
}
