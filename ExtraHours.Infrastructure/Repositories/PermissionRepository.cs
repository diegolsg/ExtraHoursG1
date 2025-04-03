

using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Repositories
{
    public class PermissionRepository : IRepository<Permission>
    {
        readonly AppDbContext _context;
        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permission>> GetAll()
        {
            return await Task.FromResult(_context.Permissions.ToList());
        }

        public async Task<Permission?> GetById(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }

        public async Task Create(Permission permission)
        {
            await _context.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Permission permission)
        {
            _context.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        { 
            var role = await GetById(id); 
            _context.Permissions.Remove(role); //Verficar
            await _context.SaveChangesAsync();
        }
    }
}