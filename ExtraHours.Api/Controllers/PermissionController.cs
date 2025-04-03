

using ExtraHours.Core.Models;
using ExtraHours.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExtraHours.Api.Controllers{
    [ApiController]
    [Route("api/[controller]")]

    public class PermissionController : ControllerBase{
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Permission>> Get()
        {
            return await _permissionService.GetAllPermissionAsync();
        }

        [HttpGet("{id}")]
        public async Task<Permission> GetById(int id)
        {
            return await _permissionService.GetPermissionById(id);
        }

        [HttpPost]
        public async Task Post([FromBody] Permission permission)
        {
            await _permissionService.CreatePermissionAsync(permission);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody] Permission permission)
        {
            if (id != permission.Id) return BadRequest();
            await _permissionService.ActualizarPermissionAsync(permission);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _permissionService.DeletePermissionAsync(id);
        }
    }

}