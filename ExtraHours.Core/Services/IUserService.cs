using ExtraHours.Core.Models;

namespace ExtraHours.Core.Services
{
    public interface IUserService 
    {
        Task<String?> Authenticate(string email, string password);    
        Task<User> Register(User user);    
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetByNameOrCodeAsync(string search);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User entity);
    }
}