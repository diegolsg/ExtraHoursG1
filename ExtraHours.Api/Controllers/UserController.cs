using ExtraHours.Core.Dto;
using ExtraHours.Core.Models;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            await _userService.Register(user);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
<<<<<<< HEAD
            var users = await _userService.GetUsers();
            return Ok(users);
=======
            return await _userService.GetAllAsync();
>>>>>>> origin/diego
        }

        [HttpPost("userCreate")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await _userService.CreateUser(user);
            return Ok(user);
        }

        [HttpGet("search/{search}")]
        public async Task<IActionResult> GetByNameOrCode(string search)
        {
            var user = await _userService.GetByNameOrCodeAsync(search);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
<<<<<<< HEAD
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateUser([FromBody] User entity)
        {
            var user = await _userService.GetUserByIdAsync(entity.Id);
            if (user == null) return NotFound();
            await _userService.UpdateUser(entity);
            return Ok(user);
        }

=======
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error en el servidor.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<UserDto> Post([FromBody] UserDto user)
        {
           return await _userService.CreateAsync(user);
        }

        [HttpPut]
        public async Task Put([FromBody] UserDto user)
        {
            await _userService.UpdateAsync(user, user.Id);
        }
>>>>>>> origin/diego
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
<<<<<<< HEAD
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            await _userService.DeleteUser(id);
            return NoContent();
=======
            await _userService.DeleteAsync(id);
>>>>>>> origin/diego
        }
    }
}