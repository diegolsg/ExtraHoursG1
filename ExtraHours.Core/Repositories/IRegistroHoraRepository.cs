using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ExtraHours.Core.Repositories
{
    public interface IRegistroHoraRepository
    {
        Task AddAsync(RegistroHora registro);
        Task<List<RegistroHora>> GetByUserAsync(int userId);

    }
}
