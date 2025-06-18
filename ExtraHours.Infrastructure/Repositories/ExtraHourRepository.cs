using ExtraHours.Core.dto;
using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
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
            return await _context.ExtraHours
                .Include(eh => eh.Users)
                .ToListAsync();
        }

        public async Task<IEnumerable<ExtraHourDto>> GetAllWithDtoAsync()
        {
            return await _context.ExtraHours
                .Include(eh => eh.Users)
                .Select(eh => new ExtraHourDto
                {
                    Id = eh.Id,
                    UserId = eh.UserId,
                    Name = eh.Users.Name,
                    Code = eh.Users.Code,
                    Date = eh.Date,
                    StartTime = eh.StartTime,
                    EndTime = eh.EndTime,
                    Status = eh.Status,
                })
                .ToListAsync();
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

        public async Task HourStatus(int id, string status)
        {
            ExtraHour extraHour = await GetByIdAsync(id);
            if (extraHour == null)
            {
                throw new KeyNotFoundException($"ExtraHour with ID {id} not found");
            }
            extraHour.Status = status;
            _context.ExtraHours.Update(extraHour);
            await _context.SaveChangesAsync();
        }
    }
}
