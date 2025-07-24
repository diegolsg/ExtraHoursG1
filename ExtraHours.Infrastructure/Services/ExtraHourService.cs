using ExtraHours.Core.dto;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ExtraHours.Infrastructure.Services
{
    public class ExtraHourService : IExtraHourService
    {
        private readonly IExtraHourRepository _extraHourRepository;
        private readonly AppDbContext _context;

        public ExtraHourService(
            IExtraHourRepository extraHourRepository,
            AppDbContext context)
        {
            _extraHourRepository = extraHourRepository;
            _context = context;
        }

        public async Task<IEnumerable<ExtraHourDto>> GetAllAsync()
        {
            var extraHours = await _extraHourRepository.GetAllAsync();

            return extraHours.Select(eh => new ExtraHourDto
            {
                Id = eh.Id,
                UserId = eh.UserId,
                Name = eh.User?.Name ?? "Usuario no encontrado",
                Code = eh.User?.Code ?? "Sin código",
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
            if (extraHour == null)
                throw new Exception("Extra Hour not found");
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
            if (extraHour == null)
                throw new Exception("Extra Hour not found");

            extraHour.Status = status;
            await _extraHourRepository.HourStatus(id, status);
        }

        public async Task<List<ExtraHourSummaryDto>> GetExtraHoursSummaryAsync()
        {
            var result = await _context.ExtraHours
                .Include(eh => eh.User)
                .Include(eh => eh.RegistroHora)
                .Select(eh => new ExtraHourSummaryDto
                {
                    Id = eh.Id,
                    UserName = eh.User != null ? eh.User.Name : "Sin nombre",
                    Code = eh.RegistroHora != null ? eh.RegistroHora.Id.ToString() : "Sin registro",
                    Fecha = eh.RegistroHora != null ? eh.RegistroHora.Fecha : "Sin fecha",
                    StartTime = eh.RegistroHora != null ? eh.RegistroHora.StartTime : "",
                    EndTime = eh.RegistroHora != null ? eh.RegistroHora.EndTime : "",
                    CantidadHoras = eh.RegistroHora != null ? eh.RegistroHora.CantidadHoras : 0,
                    Estado = eh.Status ?? "Pendiente"
                })
                .ToListAsync();

            return result;
        }
    }
}

