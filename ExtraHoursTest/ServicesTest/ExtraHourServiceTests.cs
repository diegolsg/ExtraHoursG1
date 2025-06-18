using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExtraHours.Infrastructure.Services;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Models;
using ExtraHours.Core.dto;
using System;

namespace ExtraHours.Tests.Services
{
    public class ExtraHourServiceTests
    {
        private readonly Mock<IExtraHourRepository> _mockRepo;
        private readonly ExtraHourService _service;

        public ExtraHourServiceTests()
        {
            _mockRepo = new Mock<IExtraHourRepository>();
            _service = new ExtraHourService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
            // Arrange
            var extraHours = new List<ExtraHour>
            {
                new ExtraHour
                {
                    Id = 1,
                    UserId = 2,
                    Users = new User { Id = 2, Name = "Juan", Code = "U001", PhoneNumber = "123", Password = "pass", Email = "a@a.com", Salary = 1000, RoleId = 1, Created = DateTime.Now, Updated = DateTime.Now },
                    Date = "2024-06-01",
                    StartTime = "08:00",
                    EndTime = "10:00",
                    Status = "Pendiente",
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    ExtraHoursTypeId = 1,
                    ExtraHoursType = new ExtraHourType { Id = 1, TypeHourName = "Normal", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.Now, Updated = DateTime.Now }
                }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(extraHours);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal(1, dto.Id);
            Assert.Equal("Juan", dto.Name);
            Assert.Equal("U001", dto.Code);
        }

        [Fact]
        public async Task GetAllWithDtoAsync_ReturnsDtos()
        {
            // Arrange
            var dtos = new List<ExtraHourDto>
            {
                new ExtraHourDto
                {
                    Id = 1,
                    UserId = 2,
                    Name = "Juan",
                    Code = "U001",
                    Date = "2024-06-01",
                    StartTime = "08:00",
                    EndTime = "10:00",
                    Status = "Pendiente"
                }
            };
            _mockRepo.Setup(r => r.GetAllWithDtoAsync()).ReturnsAsync(dtos);

            // Act
            var result = await _service.GetAllWithDtoAsync();

            // Assert
            var dto = Assert.Single(result);
            Assert.Equal("Juan", dto.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsExtraHour()
        {
            // Arrange
            var extraHour = new ExtraHour { Id = 1, UserId = 2, Date = "2024-06-01", StartTime = "08:00", EndTime = "10:00", Status = "Pendiente", Created = DateTime.Now, Updated = DateTime.Now };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(extraHour);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsException_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((ExtraHour)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetByIdAsync(1));
        }

        [Fact]
        public async Task AddAsync_CallsRepository()
        {
            // Arrange
            var dto = new ExtraHourDto
            {
                UserId = 2,
                Date = "2024-06-01",
                StartTime = "08:00",
                EndTime = "10:00",
                Status = "Pendiente",
                ExtraHoursTypeId = 1
            };

            // Act
            await _service.AddAsync(dto);

            // Assert
            _mockRepo.Verify(r => r.AddAsync(It.Is<ExtraHour>(e =>
                e.UserId == dto.UserId &&
                e.Date == dto.Date &&
                e.StartTime == dto.StartTime &&
                e.EndTime == dto.EndTime &&
                e.Status == dto.Status &&
                e.ExtraHoursTypeId == dto.ExtraHoursTypeId
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesFieldsAndCallsRepository()
        {
            // Arrange
            var existing = new ExtraHour { Id = 1, Date = "2024-06-01", StartTime = "08:00", EndTime = "10:00" };
            var dto = new ExtraHourDto { Date = "2024-06-02", StartTime = "09:00", EndTime = "11:00" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existing);

            // Act
            await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal("2024-06-02", existing.Date);
            Assert.Equal("09:00", existing.StartTime);
            Assert.Equal("11:00", existing.EndTime);
            _mockRepo.Verify(r => r.UpdateAsync(existing), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            // Act
            await _service.DeleteAsync(1);

            // Assert
            _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task HourStatus_UpdatesStatusAndCallsRepository()
        {
            // Arrange
            var extraHour = new ExtraHour { Id = 1, Status = "Pendiente" };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(extraHour);

            // Act
            await _service.HourStatus(1, "Aprobado");

            // Assert
            Assert.Equal("Aprobado", extraHour.Status);
            _mockRepo.Verify(r => r.HourStatus(1, "Aprobado"), Times.Once);
        }

        [Fact]
        public async Task HourStatus_ThrowsException_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((ExtraHour)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.HourStatus(1, "Aprobado"));
        }
    }
}
