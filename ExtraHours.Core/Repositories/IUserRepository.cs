using ExtraHours.Core.Models;

namespace ExtraHours.Core.Repositories {
    public interface IUserRepository 
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByCodeAsync(string code);
        Task<List<User>> GetByNameOrCodeAsync(string search);
        Task AddUserAsync(User user);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User entity);
        Task DeleteUserAsync(int id);
    }
}