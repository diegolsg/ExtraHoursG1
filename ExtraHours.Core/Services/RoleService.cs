using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Interfeces.IServices;
using ExtraHours.Core.Models;

namespace ExtraHours.Core.Services
{

    public class RoleService
    {
        readonly IRepository<Role> _roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task CreateRoleAsync(Role role)
        {
            await _roleRepository.Create(role);
        }

        public async Task DeleteRoleAsync(int id)
        {
            await _roleRepository.Delete(id);
        }

        public async Task<IEnumerable<Role>> GetAllRoleAsync()
        {
            return await _roleRepository.GetAll();
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleRepository.GetById(id);
        }

        public async Task ActualizarRoleAsync(Role role)
        {
            await _roleRepository.Update(role);
        }

    }
}
