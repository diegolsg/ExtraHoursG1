using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Interfeces.IServices;
using ExtraHours.Core.Models;

namespace ExtraHours.Core.Services
{
    public class UserService : IService<User>
    {
        readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public Task Create(User entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAll()
        {
            return _userRepository.GetAll()
                ;
        }

        public Task<User> GetById(int id)
        {
          return _userRepository.GetById(id);
        }

        public Task Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
