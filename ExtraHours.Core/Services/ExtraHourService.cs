using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.dto;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Interfeces.IServices;
using ExtraHours.Core.Models;

namespace ExtraHours.Core.Services
{
    public class ExtraHourService : IService<ExtraHourDto>
    {
        readonly IRepository<ExtraHourDto> _extraHourRepository;
        readonly IUserRepository _userRepository;
        readonly IRepository<SettingDto> _settingRepository;
        public ExtraHourService(IRepository<ExtraHourDto> extraHourRepository,IUserRepository userRepository, IRepository<SettingDto> settingRepository)
        {
            _extraHourRepository = extraHourRepository;
            _userRepository = userRepository;
            _settingRepository = settingRepository;
        }
        //el siguinete metodo se pretende con un ciclo decir si la hora es festiva o no y ralizar un contar el numero de horas para 
        public async Task<ExtraHourDto> CreateAsyncy(RegisterHourDto register)
        {
            var hours = SeparateHours.ListHours(TimeSpan.Parse(register.StarHour), TimeSpan.Parse(register.EndHour));

            User code = await _userRepository.FindByCodeAsync(register.Code);
            if (code != null && code.IsActive)
            {
                foreach.hours.

            }
            else
            {
                throw new Exception("Usuario no encontrado o inactivo");
            }

            return null;
        }

        public Task<ExtraHourDto> CreateAsync(ExtraHourDto entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ExtraHourDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExtraHourDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ExtraHourDto entity, int id)
        {
            throw new NotImplementedException();
        }
        private ExtraHour MapDtoToModel(ExtraHourDto extraHourDto)
        {
            return new ExtraHour
            {
                StartTime = extraHourDto.StartTime,
                EndTime = extraHourDto.EndTime,
                Status = extraHourDto.Status,
                date = extraHourDto.date,
                users = extraHourDto.users,
                ExtraHoursType = extraHourDto.ExtraHoursType // Add this line to fix the error
            };
        }

        private ExtraHourDto MapModelToDto(ExtraHour extraHour)
        {
            return new ExtraHourDto
            {
                StartTime = extraHour.StartTime,
                EndTime = extraHour.EndTime,
                Status = extraHour.Status,
                date = extraHour.date,
                users = extraHour.users,
                ExtraHoursType = extraHour.ExtraHoursType // Add this line to fix the error
            };
        }

        private bool IsHoliday(DateOnly date)
        {
            // Implement holiday logic here
            return false;
        }
    }
}
