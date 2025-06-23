using ExtraHours.Infrastructure.Services;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.Models;
using ExtraHours.Core.Services;
using ExtraHours.Core.dto;
using NSubstitute;

namespace ExtraHours.Tests.Services
{
    public class ExtraHourTypeServiceTests
    {
        private readonly IExtraHourTypeService _extraHourTypeService;
        private readonly IExtraHourTypeRepository _extraHourTypeRepository;

        public ExtraHourTypeServiceTests()
        {
            _extraHourTypeRepository = Substitute.For<IExtraHourTypeRepository>();
            _extraHourTypeService = new ExtraHourTypeService(_extraHourTypeRepository);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllTypes()
        {
            var types = new List<ExtraHourType>
            {
                new ExtraHourType { Id = 1, TypeHourName = "Nocturna", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.UtcNow, Updated = DateTime.UtcNow }
            };
            _extraHourTypeRepository.GetAllAsync().Returns(types);

            var result = await _extraHourTypeService.GetAllAsync();

            Assert.Single(result);
        }

        [Fact]
        public async Task GetByTypeHourNameAsync_ReturnsType()
        {
            var type = new ExtraHourType { Id = 1, TypeHourName = "Nocturna", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.UtcNow, Updated = DateTime.UtcNow };
            _extraHourTypeRepository.GetByTypeHourNameAsync("Nocturna").Returns(type);

            var result = await _extraHourTypeService.GetByTypeHourNameAsync("Nocturna");

            Assert.NotNull(result);
            Assert.Equal("Nocturna", result.TypeHourName);
        }

        [Fact]
        public async Task AddAsync_CallsRepositoryAdd()
        {
            var dto = new ExtraHourTypeDto
            {
                TypeHourName = "Nocturna",
                Porcentaje = "50%",
                StartExtraHour = "22:00:00",
                EndExtraHour = "06:00:00"
            };

            ExtraHourType? captured = null;
            _extraHourTypeRepository.AddAsync(Arg.Do<ExtraHourType>(e => captured = e)).Returns(Task.CompletedTask);

            await _extraHourTypeService.AddAsync(dto);

            Assert.NotNull(captured);
            Assert.Equal(dto.TypeHourName, captured.TypeHourName);
            Assert.Equal(dto.Porcentaje, captured.Porcentaje);
            Assert.Equal(TimeSpan.Parse(dto.StartExtraHour), captured.StartExtraHour);
            Assert.Equal(TimeSpan.Parse(dto.EndExtraHour), captured.EndExtraHour);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesAndCallsRepository()
        {
            var existing = new ExtraHourType
            {
                Id = 1,
                TypeHourName = "Nocturna",
                Porcentaje = "50%",
                StartExtraHour = TimeSpan.Zero,
                EndExtraHour = TimeSpan.Zero,
                Created = DateTime.Now,
                Updated = DateTime.Now
            };
            var dto = new ExtraHourTypeDto
            {
                TypeHourName = "Nocturna Modificada",
                Porcentaje = "60%",
                StartExtraHour = "23:00:00",
                EndExtraHour = "07:00:00"
            };
            _extraHourTypeRepository.GetByTypeHourNameAsync("Nocturna").Returns(existing);

            await _extraHourTypeService.UpdateAsync("Nocturna", dto);

            await _extraHourTypeRepository.Received(1).UpdateAsync(Arg.Is<ExtraHourType>(e =>
                e.TypeHourName == dto.TypeHourName &&
                e.Porcentaje == dto.Porcentaje &&
                e.StartExtraHour == TimeSpan.Parse(dto.StartExtraHour) &&
                e.EndExtraHour == TimeSpan.Parse(dto.EndExtraHour)
            ));
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsType_WhenExists()
        {
            var type = new ExtraHourType { Id = 1, TypeHourName = "Nocturna", Porcentaje = "50%", StartExtraHour = TimeSpan.Zero, EndExtraHour = TimeSpan.Zero, Created = DateTime.UtcNow, Updated = DateTime.UtcNow };
            _extraHourTypeRepository.GetByIdAsync(1).Returns(type);

            var result = await _extraHourTypeService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetByIdAsync_ThrowsException_WhenNotFound()
        {
            _extraHourTypeRepository.GetByIdAsync(2).Returns(Task.FromResult<ExtraHourType>(null!));

            await Assert.ThrowsAsync<Exception>(() => _extraHourTypeService.GetByIdAsync(2));
        }
    }
}
