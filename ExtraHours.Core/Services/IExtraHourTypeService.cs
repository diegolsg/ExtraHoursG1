using ExtraHours.Core.Models;
using ExtraHours.Core.dto;

namespace ExtraHours.Core.Services
{
    public interface IExtraHourTypeService
    {
        Task<IEnumerable<ExtraHourType>> GetAllAsync();
        Task<ExtraHourType> GetByTypeHourNameAsync(string typeHourName);
        Task AddAsync(ExtraHourTypeDto extraHourTypeDto);
        Task UpdateAsync(string typeHourName, ExtraHourTypeDto extraHourTypeDto);
    }
}