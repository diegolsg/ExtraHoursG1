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

        public async Task<IEnumerable<ExtraHourDto>> GetAllAsync()
        {
            var extraHours = await _repository.GetAllAsync();

            return extraHours.Select(eh => new ExtraHourDto
            {
                Id = eh.Id,
                UserId = eh.UserId,
                Name = eh.Users?.Name ?? "Usuario no encontrado",
                Code = eh.Users?.Code ?? "Sin código",
                Date = eh.Date,
                StartTime = eh.StartTime,
                EndTime = eh.EndTime,
                Status = eh.Status,
                Created = eh.Created,
                Updated = eh.Updated,
                ExtraHoursTypeId = eh.ExtraHoursTypeId
            }).ToList();
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
                Date = extraHourDto.Date,
                StartTime = extraHourDto.StartTime,
                EndTime = extraHourDto.EndTime,
                Status = extraHourDto.Status,
                ExtraHoursTypeId = extraHourDto.ExtraHoursTypeId,
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
