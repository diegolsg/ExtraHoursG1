using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRoleAsync(Role entity)
        {
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync() => await _context.Roles.ToListAsync();
    }
}
