using ExtraHours.Core.Models;
using ExtraHours.Core.Dtos;

namespace ExtraHours.Core.Interfaces.IServices
{
    public interface IExtraHourService
    {
        Task<IEnumerable<ExtraHour>> GetAllAsync();
        Task<ExtraHour> GetByIdAsync(int id);
        Task AddAsync(ExtraHourDto extraHourDto);
        Task UpdateAsync(int id, ExtraHourDto extraHourDto);
        Task DeleteAsync(int id);
    }
}