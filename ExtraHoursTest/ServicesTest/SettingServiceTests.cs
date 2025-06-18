using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtraHours.Core.dto;
using ExtraHours.Core.Models;
using ExtraHours.Core.Repositories;
using ExtraHours.Infrastructure.Services;
using Moq;
using Xunit;

namespace ExtraHours.Infrastructure.Tests.Services
{
    public class SettingServiceTests
    {
        private readonly Mock<ISettingRepository> _mockRepo;
        private readonly SettingService _service;

        public SettingServiceTests()
        {
            _mockRepo = new Mock<ISettingRepository>();
            _service = new SettingService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllSettings()
        {
            // Arrange
            var settings = new List<Setting>
            {
                new Setting { Id = 1, LimitExtraHoursDay = 2, LimitExtraHoursWeek = 10, TotalHoursWeek = 40 },
                new Setting { Id = 2, LimitExtraHoursDay = 3, LimitExtraHoursWeek = 12, TotalHoursWeek = 38 }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(settings);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Id == 1);
            Assert.Contains(result, s => s.Id == 2);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsSetting()
        {
            // Arrange
            var setting = new Setting { Id = 1, LimitExtraHoursDay = 2, LimitExtraHoursWeek = 10, TotalHoursWeek = 40 };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(setting);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ThrowsException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Setting)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(99));
        }

        [Fact]
        public async Task AddAsync_ValidDto_CallsRepositoryAdd()
        {
            // Arrange
            var dto = new SettingDto
            {
                LimitExtraHoursDay = 2,
                LimitExtraHoursWeek = 10,
                TotalHoursWeek = 40
            };

            // Act
            await _service.AddAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<Setting>(
                s => s.LimitExtraHoursDay == dto.LimitExtraHoursDay &&
                     s.LimitExtraHoursWeek == dto.LimitExtraHoursWeek &&
                     s.TotalHoursWeek == dto.TotalHoursWeek)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ExistingSetting_UpdatesAndCallsRepository()
        {
            // Arrange
            var existing = new Setting
            {
                Id = 1,
                LimitExtraHoursDay = 1,
                LimitExtraHoursWeek = 5,
                TotalHoursWeek = 35,
                Created = DateTime.UtcNow.AddDays(-1),
                Updated = DateTime.UtcNow.AddDays(-1)
            };
            var dto = new SettingDto
            {
                LimitExtraHoursDay = 3,
                LimitExtraHoursWeek = 12,
                TotalHoursWeek = 40
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Setting> { existing });

            // Act
            await _service.UpdateAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.Is<Setting>(
                s => s.LimitExtraHoursDay == dto.LimitExtraHoursDay &&
                     s.LimitExtraHoursWeek == dto.LimitExtraHoursWeek &&
                     s.TotalHoursWeek == dto.TotalHoursWeek)), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NoExistingSetting_DoesNotCallRepository()
        {
            // Arrange
            var dto = new SettingDto
            {
                LimitExtraHoursDay = 3,
                LimitExtraHoursWeek = 12,
                TotalHoursWeek = 40
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Setting>());

            // Act
            await _service.UpdateAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Setting>()), Times.Never);
        }
    }
}
