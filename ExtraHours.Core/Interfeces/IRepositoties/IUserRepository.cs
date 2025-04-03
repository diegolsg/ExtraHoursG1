using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.Models;

namespace ExtraHours.Core.Interfeces.IRepositoties
{
    public interface IUserRepository
    {
       Task<User> FindByCodeAsync(string code);
    }
}
