using ExtraHours.Core.Models;
using ExtraHours.Core.Dto;

namespace ExtraHours.Core.Interfaces.IServices
{
    public interface IExtraHoursTypeService
    {
        Task<IEnumerable<ExtraHourType>> GetAllAsync();
        Task<ExtraHourType> GetByIdAsync(int id);
        Task AddAsync(ExtraHourTypeDto extraHourTypeDto);
        Task UpdateAsync(int id, ExtraHourTypeDto extraHourTypeDto);
        Task DeleteAsync(int id);
    }
}