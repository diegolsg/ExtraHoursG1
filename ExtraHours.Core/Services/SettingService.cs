using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtraHours.Core.Interfeces.IRepositories;
using ExtraHours.Core.Interfeces.IServices;
using ExtraHours.Core.Models;
using ExtraHours.Core.dto;

namespace ExtraHours.Core.Services
{
    public class SettingsService : IService<dtoSettings>
    {
        private readonly IRepository<Settings> _settingsRepository;

        public SettingsService(IRepository<Settings> settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task CreateUserAsync(dtoSettings settingsDto)
        {
            if (settingsDto == null)
            {
                throw new ArgumentNullException(nameof(settingsDto));
            }

            var settingsModel = MapDtoToModel(settingsDto);
            await _settingsRepository.Create(settingsModel);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _settingsRepository.Delete(id);
        }

        public async Task<IEnumerable<dtoSettings>> GetAllUserAsync()
        {
            var settings = await _settingsRepository.GetAll();
            return settings.Select(MapModelToDto);
        }

        public async Task<dtoSettings> GetByIdUserAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }
            var setting = await _settingsRepository.GetById(id);
            return MapModelToDto(setting);
        }

        public async Task UpdateUserAsync(dtoSettings settingsDto, int id)
        {
            if (settingsDto == null)
            {
                throw new ArgumentNullException(nameof(settingsDto));
            }
            if (id <= 0)
            {
                throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
            }
            var settingsModel = MapDtoToModel(settingsDto);
            settingsModel.Id = id;
            await _settingsRepository.Update(settingsModel);
        }

        private Setting MapDtoToModel(dtoSettings settingsDto)
        {
            return new Setting
            {
                Id = settingsDto.Id,
                Key = settingsDto.Key,
                Value = settingsDto.Value
            };
        }

        private dtoSettings MapModelToDto(Setting settings)
        {
            return new dtoSettings
            {
                Id = settings.Id,
                Key = settings.Key,
                Value = settings.Value
            };
        }
    }
}
