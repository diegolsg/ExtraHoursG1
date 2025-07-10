using ExtraHours.Core.Models;
using ExtraHours.Core.Dto;


namespace ExtraHours.Core.Services
{
    public interface IRegistroHoraService
    {
        Task RegistrarHoraAsync(CreateExtraHourDto dto);
        Task<IEnumerable<ExtraHour>> ObtenerHorasPorUsuarioAsync(int userId);
    }
}
