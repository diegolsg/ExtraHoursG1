﻿

using ExtraHours.Core.dto;
using ExtraHours.Core.Interfeces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        readonly IService<UserDto> _userService;
        public UserController(IService<UserDto> userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get()
        {
            return await _userService.GetAllUserAsync();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var user = await _userService.GetByIdUserAsync(id);
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
        public async Task Post([FromBody] UserDto user)
        {
            await _userService.CreateUserAsync(user);
        }

        [HttpPut]
        public async Task Put([FromBody] UserDto user)
        {
            await _userService.UpdateUserAsync(user, user.Id);
        }
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _userService.DeleteUserAsync(id);
        }
    }
}
