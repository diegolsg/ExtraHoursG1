using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using ExtraHours.Infrastructure.Services;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Models;
using ExtraHours.Core.dto;

namespace ExtraHours.Tests.Services
{
    public class ExtraHourTypeServiceTests
    {
        private readonly Mock<IExtraHourTypeRepository> _mockRepo;
        private readonly ExtraHourTypeService _service;

        public ExtraHourTypeServiceTests()
        {
            _mockRepo = new Mock<IExtraHourTypeRepository>();
            _service = new ExtraHourTypeService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTypes()
        {
            // Arrange
            var types = new List<ExtraHourType>
            {
                new ExtraHourType { Id = 1, TypeHourName = "Nocturna", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(types);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByTypeHourNameAsync_ReturnsType()
        {
            // Arrange
            var type = new ExtraHourType { Id = 1, TypeHourName = "Nocturna", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.UtcNow, Updated = DateTime.UtcNow };
            _mockRepo.Setup(r => r.GetByTypeHourNameAsync("Nocturna")).ReturnsAsync(type);

            // Act
            var result = await _service.GetByTypeHourNameAsync("Nocturna");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Nocturna", result.TypeHourName);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAdd()
        {
            // Arrange
            var dto = new ExtraHourTypeDto
            {
                TypeHourName = "Nocturna",
                Porcentaje = "50%",
                StartExtraHour = "22:00:00",
                EndExtraHour = "06:00:00"
            };

            // Act
            await _service.AddAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<ExtraHourType>(e =>
                e.TypeHourName == dto.TypeHourName &&
                e.Porcentaje == dto.Porcentaje &&
                e.StartExtraHour == TimeSpan.Parse(dto.StartExtraHour) &&
                e.EndExtraHour == TimeSpan.Parse(dto.EndExtraHour)
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesAndCallsRepository()
        {
            // Arrange
            var existing = new ExtraHourType
            {
                Id = 1,
                TypeHourName = "Nocturna",
                Porcentaje = "50%",
                StartExtraHour = TimeSpan.Zero,
                EndExtraHour = TimeSpan.Zero,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow
            };
            var dto = new ExtraHourTypeDto
            {
                TypeHourName = "Nocturna Modificada",
                Porcentaje = "60%",
                StartExtraHour = "23:00:00",
                EndExtraHour = "07:00:00"
            };
            _mockRepo.Setup(r => r.GetByTypeHourNameAsync("Nocturna")).ReturnsAsync(existing);

            // Act
            await _service.UpdateAsync("Nocturna", dto);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<ExtraHourType>(e =>
                e.TypeHourName == dto.TypeHourName &&
                e.Porcentaje == dto.Porcentaje &&
                e.StartExtraHour == TimeSpan.Parse(dto.StartExtraHour) &&
                e.EndExtraHour == TimeSpan.Parse(dto.EndExtraHour)
            )), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsType_WhenExists()
        {
            // Arrange
            var type = new ExtraHourType { Id = 1, TypeHourName = "Nocturna", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.UtcNow, Updated = DateTime.UtcNow };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(type);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsException_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync((ExtraHourType)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(2));
        }
    }
}
