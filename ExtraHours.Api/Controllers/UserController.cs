using ExtraHours.Core.Models;
using ExtraHours.Core.dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly EmailService _emailService;
        public UserController(IUserService userService, EmailService emailService)
        {
            _userService = userService;
            _emailService = emailService;
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
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("userCreate")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = new User
            {
                Name = userDto.Name,
                PhoneNumber = userDto.PhoneNumber,
                Email = userDto.Email,
                Salary = userDto.Salary,
                RoleId = 2, 
                Password = "" 
            };

            await _emailService.SendCodeForEmailAsync(user.Email, user.Name, user.Code);

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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            await _userService.DeleteUser(id);
            return NoContent();
        }

        [HttpGet("code")]
        public async Task<IActionResult> GetUserByCode([FromQuery] string code)
        {
            var user = await _userService.GetByCodeAsync(code);
            if (user == null)
            {
                return NotFound($"No se encontró un usuario con el código {code}");
            }
            return Ok(new { userId = user.Id, name = user.Name });
        }
    }
}