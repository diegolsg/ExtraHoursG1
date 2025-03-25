using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.Models;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Infrastructure.Data;


namespace ExtraHours.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        readonly AppDbContex _context;
        public UserRepository(AppDbContex context)
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
