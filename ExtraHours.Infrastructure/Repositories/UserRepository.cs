


using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;

namespace ExtraHours.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
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
        public Task<User> GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(user);
        }
        public Task Create(User entity)
        {
            throw new NotImplementedException();
        }
        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
