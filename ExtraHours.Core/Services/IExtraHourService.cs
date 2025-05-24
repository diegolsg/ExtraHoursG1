using ExtraHours.Core.Models;
using ExtraHours.Core.Dto;

namespace ExtraHours.Core.Services
{
    public interface IExtraHourService
    {
        Task<IEnumerable<ExtraHourDto>> GetAllAsync();
        Task<ExtraHour> GetByIdAsync(int id);
        Task AddAsync(ExtraHourDto extraHourDto);
        Task UpdateAsync(int id, ExtraHourDto extraHourDto);
        Task DeleteAsync(int id);
    }
}