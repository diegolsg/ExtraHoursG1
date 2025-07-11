using ExtraHours.Core.Models;
using ExtraHours.Core.dto;

namespace ExtraHours.Core.Services
{
    public interface IExtraHourService
    {
        Task<IEnumerable<ExtraHourDto>> GetAllAsync();
        Task<IEnumerable<ExtraHourDto>> GetAllWithDtoAsync();
        Task<ExtraHour> GetByIdAsync(int id);
        Task AddAsync(ExtraHourDto extraHourDto);
        Task UpdateAsync(int id, ExtraHourDto extraHourDto);
        Task DeleteAsync(int id);
        Task HourStatus(int id, string status);
    }
}