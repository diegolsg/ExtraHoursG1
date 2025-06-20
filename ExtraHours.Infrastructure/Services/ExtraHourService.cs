using ExtraHours.Core.dto;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Services
{
    public class ExtraHourService : IExtraHourService
    {
        private readonly IExtraHourRepository _extraHourRepository;
        public ExtraHourService(IExtraHourRepository extraHourRepository)
        {
            _extraHourRepository = extraHourRepository;
        }

        public async Task<IEnumerable<ExtraHourDto>> GetAllAsync()
        {
            var extraHours = await _extraHourRepository.GetAllAsync();

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
            }).ToList();
        }

        public async Task<IEnumerable<ExtraHourDto>> GetAllWithDtoAsync()
        {
            var extraHours = await _extraHourRepository.GetAllWithDtoAsync();

            return extraHours.Select(eh => new ExtraHourDto
            {
                Id = eh.Id,
                UserId = eh.UserId,
                Name = eh.Name,
                Code = eh.Code,
                Date = eh.Date,
                StartTime = eh.StartTime,
                EndTime = eh.EndTime,
                Status = eh.Status
            }).ToList();
        }

        public async Task<ExtraHour> GetByIdAsync(int id)
        {
            var extraHour = await _extraHourRepository.GetByIdAsync(id);
            if (extraHour == null) throw new Exception("Extra Hour not found");
            return extraHour;
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
            };

            await _extraHourRepository.AddAsync(extraHour);
        }

        public async Task UpdateAsync(int id, ExtraHourDto extraHourDto)
        {
            var extraHour = await _extraHourRepository.GetByIdAsync(id);
            if (extraHour != null)
            {
                extraHour.Date = extraHourDto.Date;
                extraHour.StartTime = extraHourDto.StartTime;
                extraHour.EndTime = extraHourDto.EndTime;

                await _extraHourRepository.UpdateAsync(extraHour);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _extraHourRepository.DeleteAsync(id);
        }

        public async Task HourStatus(int id, string status)
        {
            var extraHour = await _extraHourRepository.GetByIdAsync(id);
            if (extraHour == null) throw new Exception("Extra Hour not found");

            extraHour.Status = status;
            await _extraHourRepository.HourStatus(id, status);
        }
    }
}
