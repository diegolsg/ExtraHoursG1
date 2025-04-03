using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Repositories
{
    public class SettingRepository:IRepository<Setting>
    {
        readonly AppDbContext _context;
        public SettingRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<Setting> Create(Setting entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setting>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Setting?> GetById(int id)
        {
            var setting = _context.Settings.Find(id);
            return Task.FromResult(setting);
        }

        public Task Update(Setting entity)
        {
            throw new NotImplementedException();
        }
    }
}
