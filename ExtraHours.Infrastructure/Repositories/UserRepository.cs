


using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.FromResult(_context.Users.ToList());
        }
        public async Task<User> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found");
            }
            return user;
        }
        public async Task<User> Create(User entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task Update(User entity)
        {
            var userExist = await _context.Users
                .FirstOrDefaultAsync(p => p.Id == entity.Id);

            if (userExist == null)
            {
                throw new KeyNotFoundException($"El usuario con ID {entity.Id} no existe.");
            }

            userExist.Name = entity.Name;
            userExist.Password = entity.Password;
            userExist.Email = entity.Email;
            userExist.PhoneNumber = entity.PhoneNumber;
            userExist.Code = entity.Code;
            userExist.AreaId = entity.AreaId;
            userExist.RoleId = entity.RoleId;

            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            User user = await GetById(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindByCodeAsync(string code)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Code == code);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with code {code} not found");
            }
            return user;
        }
    }
}
