using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraHours.Core.Interfeces.IServices
{
    public interface IUserService
    {
        Task FindByCodeAsync(string code);
    }
}
