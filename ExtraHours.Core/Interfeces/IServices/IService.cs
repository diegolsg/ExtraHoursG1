using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraHours.Core.Interfeces.IServices
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllUserAsync();
        Task<T> GetByIdUserAsync(int id);
        Task CreateUserAsync(T entity);
        Task UpdateUserAsync(T entity, int id);
        Task DeleteUserAsync(int id);
    }
}
