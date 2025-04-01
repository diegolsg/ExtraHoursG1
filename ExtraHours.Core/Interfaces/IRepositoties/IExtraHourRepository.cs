using ExtraHours.Core.Models;
using ExtraHours.Core.Dtos;

namespace ExtraHours.Core.Interfaces.IRepositories
{
    public interface IExtraHourRepository
    {
        Task<IEnumerable<ExtraHour>> GetAllAsync();
        Task<ExtraHour> GetByIdAsync(int id);
        Task AddAsync(ExtraHour extraHour);
        Task UpdateAsync(ExtraHour extraHour);
        Task DeleteAsync(int id);
    }
}