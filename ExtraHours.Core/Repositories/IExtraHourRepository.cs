using ExtraHours.Core.Models;

namespace ExtraHours.Core.Repositories
{
    public interface IExtraHourRepository
    {
        Task<IEnumerable<ExtraHour>> GetAllAsync();
        Task<ExtraHour> GetByIdAsync(int id);
        Task AddAsync(ExtraHour extraHour);
        Task UpdateAsync(ExtraHour extraHour);
        Task DeleteAsync(int id);
        Task HourStatus (int id, string status);
    }
}