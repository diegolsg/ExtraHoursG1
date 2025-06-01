using ExtraHours.Core.dto;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;

using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task<IEnumerable<Setting>> GetAllAsync()
        {
            return await _settingRepository.GetAllAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            var setting = await _settingRepository.GetByIdAsync(id);
            if (setting == null) throw new Exception("Setting not found");
            return setting;
        }

        public async Task AddAsync(SettingDto settingDto)
        {
            var setting = new Setting
            {
                LimitExtraHoursDay = settingDto.LimitExtraHoursDay,
                LimitExtraHoursWeek = settingDto.LimitExtraHoursWeek,
                TotalHoursWeek = settingDto.TotalHoursWeek,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _settingRepository.AddAsync(setting);
        }

        public async Task UpdateAsync(SettingDto settingDto)
        {
            var settings = await _settingRepository.GetAllAsync();
            var setting = settings.FirstOrDefault();
            if (setting != null)
            {
                setting.LimitExtraHoursDay = settingDto.LimitExtraHoursDay;
                setting.LimitExtraHoursWeek = settingDto.LimitExtraHoursWeek;
                setting.TotalHoursWeek = settingDto.TotalHoursWeek;
                setting.Updated = DateTime.UtcNow;

                await _settingRepository.UpdateAsync(setting);
            }
        }
    }
}
