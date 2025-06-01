using ExtraHours.Core.dto;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;

using ExtraHours.Core.Models;

namespace ExtraHours.Infrastructure.Services
{
    public class ExtraHourTypeService : IExtraHourTypeService
    {
        private readonly IExtraHourTypeRepository _extraHourTypeRepository;

        public ExtraHourTypeService(IExtraHourTypeRepository extraHourTypeRepository)
        {
            _extraHourTypeRepository = extraHourTypeRepository;
        }

        public async Task<IEnumerable<ExtraHourType>> GetAllAsync()
        {
            return await _extraHourTypeRepository.GetAllAsync();
        }

        public async Task<ExtraHourType> GetByTypeHourNameAsync(string typeHourName)
        {
            return await _extraHourTypeRepository.GetByTypeHourNameAsync(typeHourName);
        }

        public async Task AddAsync(ExtraHourTypeDto extraHourTypeDto)
        {
            var extraHourType = new ExtraHourType
            {
                TypeHourName = extraHourTypeDto.TypeHourName,
                Porcentaje = extraHourTypeDto.Porcentaje,
                StartExtraHour = TimeSpan.Parse(extraHourTypeDto.StartExtraHour),
                EndExtraHour = TimeSpan.Parse(extraHourTypeDto.EndExtraHour),
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };

            await _extraHourTypeRepository.AddAsync(extraHourType);
        }

        public async Task UpdateAsync(string typeHourName, ExtraHourTypeDto extraHourTypeDto)
        {
            var extraHourType = await _extraHourTypeRepository.GetByTypeHourNameAsync(typeHourName);
            if (extraHourType != null)
            {
                extraHourType.TypeHourName = extraHourTypeDto.TypeHourName;
                extraHourType.Porcentaje = extraHourTypeDto.Porcentaje;
                extraHourType.StartExtraHour = TimeSpan.Parse(extraHourTypeDto.StartExtraHour);
                extraHourType.EndExtraHour = TimeSpan.Parse(extraHourTypeDto.EndExtraHour);
                extraHourType.Updated = DateTime.UtcNow;

                await _extraHourTypeRepository.UpdateAsync(extraHourType);
            }
        }
    }
}
