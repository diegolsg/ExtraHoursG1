

using ExtraHours.Core.Models;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace ExtraHours.Api.Controllers{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase{
    private readonly RoleService _roleService;

    public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IEnumerable<Role>> Get()
        {
            return await _roleService.GetAllRoleAsync();
        }

        [HttpGet("{id}")]
        public async Task<Role> GetById(int id)
        {
            return await _roleService.GetRoleById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Role role)
        {
            await _roleService.CreateRoleAsync(role);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Role role)
        {
            if (id != role.Id) return BadRequest();
            await _roleService.ActualizarRoleAsync(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _roleService.DeleteRoleAsync(id);
        }
    }
}