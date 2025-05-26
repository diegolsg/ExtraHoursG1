using ExtraHours.Core.Models;

namespace ExtraHours.Core.Services
{
    public interface IRoleService 
    {  
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role> CreateRole(Role role);
    }
}