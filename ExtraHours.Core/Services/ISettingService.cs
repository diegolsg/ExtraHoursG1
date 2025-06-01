using ExtraHours.Core.Models;
using ExtraHours.Core.dto;

namespace ExtraHours.Core.Services
{
    public interface ISettingService
    {
        Task<IEnumerable<Setting>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
        Task AddAsync(SettingDto settingDtoDto);
        Task UpdateAsync(SettingDto settingDto);
    }
}