using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;
using ExtraHours.Core.dto;
using ExtraHours.Infrastructure.Services;
using NSubstitute;

namespace ExtraHours.Tests.Services
{
    public class ExtraHourServiceTests
    {
        private readonly IExtraHourService _extraHourService;
        private readonly IExtraHourRepository _extraHourRepository;

        public ExtraHourServiceTests()
        {
            _extraHourRepository = Substitute.For<IExtraHourRepository>();
            _extraHourService = new ExtraHourService(_extraHourRepository);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMappedDtos()
        {
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
                }
            };
            _extraHourRepository.GetAllAsync().Returns(extraHours);

            var result = await _extraHourService.GetAllAsync();

            var dto = Assert.Single(result);
            Assert.Equal(1, dto.Id);
            Assert.Equal("Juan", dto.Name);
            Assert.Equal("U001", dto.Code);
        }

        [Fact]
        public async Task GetAllWithDtoAsync_ReturnsDtos()
        {
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
            _extraHourRepository.GetAllWithDtoAsync().Returns(dtos);

            var result = await _extraHourService.GetAllWithDtoAsync();

            var dto = Assert.Single(result);
            Assert.Equal("Juan", dto.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsExtraHour()
        {
            var extraHour = new ExtraHour { Id = 1, UserId = 2, Date = "2024-06-01", StartTime = "08:00", EndTime = "10:00", Status = "Pendiente", Created = DateTime.Now, Updated = DateTime.Now };
            _extraHourRepository.GetByIdAsync(1).Returns(extraHour);

            var result = await _extraHourService.GetByIdAsync(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsException_WhenNotFound()
        {
            _extraHourRepository.GetByIdAsync(1).Returns((ExtraHour)null);

            await Assert.ThrowsAsync<Exception>(() => _extraHourService.GetByIdAsync(1));
        }

        [Fact]
        public async Task AddAsync_CallsRepository()
        {
            var dto = new ExtraHourDto
            {
                UserId = 2,
                Date = "2024-06-01",
                StartTime = "08:00",
                EndTime = "10:00",
                Status = "Pendiente",
            };

            ExtraHour? captured = null;
            _extraHourRepository.AddAsync(Arg.Do<ExtraHour>(e => captured = e)).Returns(Task.CompletedTask);

            await _extraHourService.AddAsync(dto);

            Assert.NotNull(captured);
            Assert.Equal(dto.UserId, captured.UserId);
            Assert.Equal(dto.Date, captured.Date);
            Assert.Equal(dto.StartTime, captured.StartTime);
            Assert.Equal(dto.EndTime, captured.EndTime);
            Assert.Equal(dto.Status, captured.Status);
            Assert.True(captured.Created <= DateTime.Now);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesFieldsAndCallsRepository()
        {
            var existing = new ExtraHour { Id = 1, Date = "2024-06-01", StartTime = "08:00", EndTime = "10:00" };
            var dto = new ExtraHourDto { Date = "2024-06-02", StartTime = "09:00", EndTime = "11:00" };
            _extraHourRepository.GetByIdAsync(1).Returns(existing);

            await _extraHourService.UpdateAsync(1, dto);

            Assert.Equal("2024-06-02", existing.Date);
            Assert.Equal("09:00", existing.StartTime);
            Assert.Equal("11:00", existing.EndTime);
            await _extraHourRepository.Received(1).UpdateAsync(existing);
        }

        [Fact]
        public async Task DeleteAsync_CallsRepository()
        {
            await _extraHourService.DeleteAsync(1);

            await _extraHourRepository.Received(1).DeleteAsync(1);
        }

        [Fact]
        public async Task HourStatus_UpdatesStatusAndCallsRepository()
        {
            var extraHour = new ExtraHour { Id = 1, Status = "Pendiente" };
            _extraHourRepository.GetByIdAsync(1).Returns(extraHour);

            await _extraHourService.HourStatus(1, "Aprobado");

            Assert.Equal("Aprobado", extraHour.Status);
            await _extraHourRepository.Received(1).HourStatus(1, "Aprobado");
        }

        [Fact]
        public async Task HourStatus_ThrowsException_WhenNotFound()
        {
            _extraHourRepository.GetByIdAsync(1).Returns((ExtraHour)null);

            await Assert.ThrowsAsync<Exception>(() => _extraHourService.HourStatus(1, "Aprobado"));
        }
    }
}
