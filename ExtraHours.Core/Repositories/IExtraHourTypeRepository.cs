using ExtraHours.Core.Models;

namespace ExtraHours.Core.Repositories
{
    public interface IExtraHourTypeRepository
    {
        Task<IEnumerable<ExtraHourType>> GetAllAsync();
        Task<ExtraHourType> GetByTypeHourNameAsync(string typeHourName);
        Task<ExtraHourType> GetByIdAsync(int id);
        Task AddAsync(ExtraHourType extraHourType);
        Task UpdateAsync(ExtraHourType extraHourType);
    }
}