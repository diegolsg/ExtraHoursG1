using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Repositories
{

    public class RoleRepository : IRepository<Role>
    {
        readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await Task.FromResult(_context.Roles.ToList());
        }

        public async Task<Role?> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task Create(Role role)
        {
            await _context.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Role role)
        {
            _context.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        { 
            var role = await GetById(id); 
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }
    }
}