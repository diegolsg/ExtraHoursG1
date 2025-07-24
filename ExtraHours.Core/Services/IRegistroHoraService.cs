using ExtraHours.Core.Models;
using ExtraHours.Core.dto;


namespace ExtraHours.Core.Services
{
    public interface IRegistroHoraService
    {
        Task RegistrarHoraAsync(CreateExtraHourDto dto);
        Task<IEnumerable<RegistroHora>> ObtenerHorasPorUsuarioAsync(int userId);
        Task<IEnumerable<ExtraHourSummaryDto>> ObtenerHorasConUsuarioAsync();


    }
}
