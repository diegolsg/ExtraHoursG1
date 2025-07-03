using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;
using ExtraHours.Infrastructure.Services;
using NSubstitute;

namespace ExtraHours.Infrastructure.Tests.Services
{
    public class RoleServiceTests
    {
        private readonly IRoleService _roleService;
        private readonly IRoleRepository _roleRepository;

        public RoleServiceTests()
        {
           _roleRepository = Substitute.For<IRoleRepository>();
            _roleService = new RoleService(_roleRepository);
        }

        [Fact]
        public async Task GetAllRolesAsync_ReturnsMappedRoles()
        {
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = null }
            };
            _roleRepository.GetAllRolesAsync().Returns(roles);

            var result = await _roleService.GetAllRolesAsync();

            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("Admin", resultList[0].Name);
            Assert.Equal("Rol no encontrado", resultList[1].Name);
        }

        [Fact]
        public async Task CreateRole_CallsRepositoryAndReturnsRole()
        {
            var role = new Role { Id = 3, Name = "User" };

            _roleRepository.AddRoleAsync(role).Returns(Task.CompletedTask);

            var result = await _roleService.CreateRole(role);

            await _roleRepository.Received(1).AddRoleAsync(role);
            Assert.Equal(role, result);
        }
    }
}
