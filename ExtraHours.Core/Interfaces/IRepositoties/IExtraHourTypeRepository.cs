namespace ExtraHours.Core.Interfaces.IRepositories
{
    public interface IExtraHourTypeRepository
    {
        Task<IEnumerable<ExtraHourType>> GetAllAsync();
        Task<ExtraHourType> GetByIdAsync(int id);
        Task AddAsync(ExtraHourType extraHourType);
        Task UpdateAsync(ExtraHourType extraHourType);
        Task DeleteAsync(int id);
    }
}