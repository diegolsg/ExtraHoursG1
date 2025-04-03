

using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;

namespace ExtraHours.Core.Services
{
    public class PermissionService
    {
        readonly IRepository<Permission> _permissionRepository;
        public PermissionService(IRepository<Permission> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task CreatePermissionAsync(Permission permission)
        {
            await _permissionRepository.Create(permission);
        }

        public async Task DeletePermissionAsync(int id)
        {
            await _permissionRepository.Delete(id);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionAsync()
        {
            return await _permissionRepository.GetAll();
        }

        public async Task<Permission> GetPermissionById(int id)
        {
            return await _permissionRepository.GetById(id);
        }

        public async Task ActualizarPermissionAsync(Permission permission)
        {
            await _permissionRepository.Update(permission);
        }

    }

}