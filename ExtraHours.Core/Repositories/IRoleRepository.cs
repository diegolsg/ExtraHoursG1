using ExtraHours.Core.Models;

namespace ExtraHours.Core.Repositories {
    public interface IRoleRepository 
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task AddRoleAsync(Role role);
    }
}