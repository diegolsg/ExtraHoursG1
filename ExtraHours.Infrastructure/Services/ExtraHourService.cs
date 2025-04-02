using ExtraHours.Core.Dto;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Services
{
    public class ExtraHourService : IExtraHourService
    {
        private readonly IExtraHourRepository _repository;

        public ExtraHourService(IExtraHourRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ExtraHour>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ExtraHour> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(ExtraHourDto extraHourDto)
        {
            var extraHour = new ExtraHour
            {
                UserId = extraHourDto.UserId,
                Code = extraHourDto.Code,
                Date = extraHourDto.Date,
                StartTime = extraHourDto.StartTime,
                EndTime = extraHourDto.EndTime, 
            };

            await _repository.AddAsync(extraHour);
        }

        public async Task UpdateAsync(int id, ExtraHourDto extraHourDto)
        {
            var extraHour = await _repository.GetByIdAsync(id);
            if (extraHour != null)
            {
                extraHour.Date = extraHourDto.Date;
                extraHour.StartTime = extraHourDto.StartTime;
                extraHour.EndTime = extraHourDto.EndTime;

                await _repository.UpdateAsync(extraHour);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
