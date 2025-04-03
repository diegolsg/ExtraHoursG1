<<<<<<< HEAD
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class ExtraHourRepository : IExtraHourRepository
    {
        private readonly AppDbContext _context;

=======
﻿using System;
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
>>>>>>> origin/diego
        public ExtraHourRepository(AppDbContext context)
        {
            _context = context;
        }

<<<<<<< HEAD
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
=======
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
>>>>>>> origin/diego
        }
    }
}
