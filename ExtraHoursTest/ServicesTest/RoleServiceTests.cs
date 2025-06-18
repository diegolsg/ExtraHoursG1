using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Services;
using Moq;
using Xunit;

namespace ExtraHours.Infrastructure.Tests.Services
{
    public class RoleServiceTests
    {
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly RoleService _roleService;

        public RoleServiceTests()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _roleService = new RoleService(_roleRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllRolesAsync_ReturnsMappedRoles()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = null }
            };
            _roleRepositoryMock.Setup(r => r.GetAllRolesAsync())
                .ReturnsAsync(roles);

            // Act
            var result = await _roleService.GetAllRolesAsync();

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count);
            Assert.Equal("Admin", resultList[0].Name);
            Assert.Equal("Rol no encontrado", resultList[1].Name);
        }

        [Fact]
        public async Task CreateRole_CallsRepositoryAndReturnsRole()
        {
            // Arrange
            var role = new Role { Id = 3, Name = "User" };

            _roleRepositoryMock.Setup(r => r.AddRoleAsync(role))
                .Returns(Task.CompletedTask)
                .Verifiable();

            // Act
            var result = await _roleService.CreateRole(role);

            // Assert
            _roleRepositoryMock.Verify(r => r.AddRoleAsync(role), Times.Once);
            Assert.Equal(role, result);
        }
    }
}
