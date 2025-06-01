using ExtraHours.Core.Models;

namespace ExtraHours.Core.Repositories
{
    public interface IExtraHourTypeRepository
    {
        Task<IEnumerable<ExtraHourType>> GetAllAsync();
        Task<ExtraHourType> GetByTypeHourNameAsync(string typeHourName);
        Task AddAsync(ExtraHourType extraHourType);
        Task UpdateAsync(ExtraHourType extraHourType);
    }
}