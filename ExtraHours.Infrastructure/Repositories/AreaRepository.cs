using ExtraHours.Core.Models;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExtraHours.Infrastructure.Repositories
{
    public class AreaRepository : IRepository<Area>
    {
        readonly AppDbContex _context;

        public AreaRepository(AppDbContex context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Area>> GetAll()
        {
            return await Task.FromResult(_context.Areas.ToList());
        }

        public Task<Area> GetById(int id)
        {
            var area = _context.Areas.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(area);
        }

        public Task Create(Area entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(Area entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}