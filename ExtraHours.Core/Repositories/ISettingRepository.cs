using ExtraHours.Core.Models;

namespace ExtraHours.Core.Repositories 
{
    public interface ISettingRepository
    {
        Task<IEnumerable<Setting>> GetAllAsync();
        Task<Setting> GetByIdAsync(int id);
        Task AddAsync(Setting setting);
        Task UpdateAsync(Setting setting);
        Task DeleteAsync(int id);    
    }
}