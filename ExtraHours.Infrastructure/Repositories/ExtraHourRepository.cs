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
    public class ExtraHourRepository : IRepository<ExtraHour>
    {
        readonly AppDbContext _context;
        public ExtraHourRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(ExtraHour entity)
        {
            await _context.ExtraHours.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            ExtraHour extraHour = await GetById(id);
            _context.ExtraHours.Remove(extraHour);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ExtraHour>> GetAll()
        {
            return await Task.FromResult(_context.ExtraHours.ToList());
        }

        public async Task<ExtraHour> GetById(int id)
        {
            var extraHour = await _context.ExtraHours.FindAsync(id);
            if (extraHour == null)
            {
                throw new KeyNotFoundException($"ExtraHour with ID {id} not found");
            }
            return extraHour;
        }

        public Task Update(ExtraHour entity)
        {
            throw new NotImplementedException();
        }
    }
}
