using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Interfeces.IServices;
using ExtraHours.Core.Models;
using ExtraHours.Core.dto;

namespace ExtraHours.Core.Services
{
    public class UserService : IService<UserDto>
    {
        readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task CreateUserAsync(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }

            var userModel = MapDtoToModel(userDto);
            await _userRepository.Create(userModel);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAll();
            return users.Select(MapModelToDto);
        }

        public async Task<UserDto> GetByIdUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }
            var user = await _userRepository.GetById(id);
            return MapModelToDto(user);
        }

        public async Task UpdateUserAsync(UserDto userDto, int id)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }
            var userModel = MapDtoToModel(userDto);
            userModel.Id = id;
            await _userRepository.Update(userModel);
        }

        private User MapDtoToModel(UserDto userDto)
        {
            return new User
            {
                Name = userDto.Name,
                PhoneNumber = userDto.PhoneNumber,
                Code = userDto.Code,
                Password = userDto.Password,
                Email = userDto.Email,
                IsActive = userDto.IsActive,
                RoleId = userDto.RoleId,
                AreaId = userDto.AreaId
            };
        }

        private UserDto MapModelToDto(User user)
        {
            return new UserDto
            {
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Code = user.Code,
                Password = user.Password,
                Email = user.Email,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                AreaId = user.AreaId
            };
        }
    }
}
