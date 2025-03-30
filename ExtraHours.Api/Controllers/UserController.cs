

using ExtraHours.Core.Interfeces.IServices;
using ExtraHours.Core.Models;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController
    {
        readonly IService<User> _userService;
        public UserController(IService<User> userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _userService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _userService.GetById(id);
        }


    }
}
