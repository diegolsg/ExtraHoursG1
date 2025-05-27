using ExtraHours.Core.Models;
using ExtraHours.Core.dto;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpPost("rolesCreate")]
        public async Task<IActionResult> CreateRoles([FromBody] RolesDto rolesDto)
        {
            var roles = new Role
            {
                Id = rolesDto.Id,
                Name = rolesDto.Name,
            };

            await _roleService.CreateRole(roles);
            return Ok(roles);
        }
    }
}