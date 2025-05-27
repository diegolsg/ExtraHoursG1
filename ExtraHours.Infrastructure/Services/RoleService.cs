using ExtraHours.Core.dto;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();

            return roles.Select(eh => new Role
            {
                Id = eh.Id,
                Name = eh.Name ?? "Rol no encontrado",
            }).ToList();
        }

        public async Task<Role> CreateRole(Role role)
        {
            await _roleRepository.AddRoleAsync(role);
            return role;
        }
    }
}
