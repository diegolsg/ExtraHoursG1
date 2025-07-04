using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ExtraHours.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;


        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _configuration = configuration;
        }

        public async Task<IEnumerable<User>> GetUsers() => await _userRepository.GetAllUsersAsync();

        public async Task<string?> Authenticate(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) return null;
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success ? GenerateJwtToken(user) : null;
        }

        public async Task<User> Register(User user)
        {
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            await _userRepository.AddUserAsync(user);
            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "secret_key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.RoleId.ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddHours(2), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User> CreateUser(User user)
        {
            user.RoleId = 2;
            user.Password = "";

            await _userRepository.CreateUserAsync(user);
            return user;
        }

        public async Task<List<User>> GetByNameOrCodeAsync(string search)
        {
            var users = await _userRepository.GetByNameOrCodeAsync(search);
            var user = users?.ToList();
            if (user == null) throw new Exception("User not found");
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            return user;
        }

        public async Task<User> UpdateUser(User entity)
        {
            var userExist = await _userRepository.GetUserByIdAsync(entity.Id);
            if (userExist == null) throw new Exception("User not found");

            userExist.Name = entity.Name;
            userExist.Email = entity.Email;
            userExist.PhoneNumber = entity.PhoneNumber;
            userExist.Salary = entity.Salary;
            await _userRepository.UpdateUserAsync(userExist);
            return userExist;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");
            await _userRepository.DeleteUserAsync(id);
        }

        public async Task<User> GetByCodeAsync(string code)
        {
            var user = await _userRepository.GetByCodeAsync(code);
            if (user == null) throw new Exception("User not found");
            return user;
        }
    }
}