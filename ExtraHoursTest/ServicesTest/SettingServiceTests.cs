using ExtraHours.Core.Repositories;
using ExtraHours.Core.Services;
using ExtraHours.Core.Models;
using ExtraHours.Core.dto;
using ExtraHours.Infrastructure.Services;
using NSubstitute;

namespace ExtraHours.Infrastructure.Tests.Services
{
    public class SettingServiceTests
    {
        private readonly ISettingService _settingService;
        private readonly ISettingRepository _settingRepository;

        public SettingServiceTests()
        {
            _settingRepository = Substitute.For<ISettingRepository>();
            _settingService = new SettingService(_settingRepository);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllSettings()
        {
            var settings = new List<Setting>
            {
                new Setting { Id = 1, LimitExtraHoursDay = 2, LimitExtraHoursWeek = 10, TotalHoursWeek = 40 },
                new Setting { Id = 2, LimitExtraHoursDay = 3, LimitExtraHoursWeek = 12, TotalHoursWeek = 38 }
            };
            _settingRepository.GetAllAsync().Returns(settings);

            var result = await _settingService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, s => s.Id == 1);
            Assert.Contains(result, s => s.Id == 2);
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsSetting()
        {
            var setting = new Setting { Id = 1, LimitExtraHoursDay = 2, LimitExtraHoursWeek = 10, TotalHoursWeek = 40 };
            _settingRepository.GetByIdAsync(1).Returns(setting);

            var result = await _settingService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(2, result.LimitExtraHoursDay);
            Assert.Equal(10, result.LimitExtraHoursWeek);
            Assert.Equal(40, result.TotalHoursWeek);
        }

        [Fact]
        public async Task GetByIdAsync_NonExistingId_ThrowsException()
        {
            _settingRepository.GetByIdAsync(20).Returns(Task.FromResult<Setting>(null!));

            await Assert.ThrowsAsync<Exception>(() => _settingService.GetByIdAsync(20));
        }

        [Fact]
        public async Task AddAsync_ValidDto_CreatesSettingWithCorrectFields()
        {
            var dto = new SettingDto
            {
                LimitExtraHoursDay = 2,
                LimitExtraHoursWeek = 10,
                TotalHoursWeek = 40
            };

            Setting? captured = null;
            _settingRepository.AddAsync(Arg.Do<Setting>(s => captured = s)).Returns(Task.CompletedTask);

            await _settingService.AddAsync(dto);

            Assert.NotNull(captured);
            Assert.Equal(dto.LimitExtraHoursDay, captured!.LimitExtraHoursDay);
            Assert.Equal(dto.LimitExtraHoursWeek, captured.LimitExtraHoursWeek);
            Assert.Equal(dto.TotalHoursWeek, captured.TotalHoursWeek);
        }

        [Fact]
        public async Task UpdateAsync_ExistingSetting_UpdatesFieldsPreservingCreated()
        {
            var createdAt = DateTime.Now.AddDays(-3);
            var updatedAt = DateTime.Now.AddDays(-1);
            var existing = new Setting
            {
                Id = 1,
                LimitExtraHoursDay = 1,
                LimitExtraHoursWeek = 5,
                TotalHoursWeek = 35,
                Created = createdAt,
                Updated = updatedAt
            };

            var dto = new SettingDto
            {
                LimitExtraHoursDay = 3,
                LimitExtraHoursWeek = 12,
                TotalHoursWeek = 40
            };

            _settingRepository.GetAllAsync().Returns(new List<Setting> { existing });

            Setting? captured = null;
            _settingRepository.UpdateAsync(Arg.Do<Setting>(s => captured = s)).Returns(Task.CompletedTask);

            await _settingService.UpdateAsync(dto);

            Assert.NotNull(captured);
            Assert.Equal(dto.LimitExtraHoursDay, captured!.LimitExtraHoursDay);
            Assert.Equal(dto.LimitExtraHoursWeek, captured.LimitExtraHoursWeek);
            Assert.Equal(dto.TotalHoursWeek, captured.TotalHoursWeek);
            Assert.Equal(createdAt, captured.Created); 
            Assert.True(captured.Updated > updatedAt); 
        }

        [Fact]
        public async Task UpdateAsync_NoExistingSetting_DoesNotCallUpdate()
        {
            var dto = new SettingDto
            {
                LimitExtraHoursDay = 3,
                LimitExtraHoursWeek = 12,
                TotalHoursWeek = 40
            };
            _settingRepository.GetAllAsync().Returns(new List<Setting>());

            await _settingService.UpdateAsync(dto);

            await _settingRepository.DidNotReceive().UpdateAsync(Arg.Any<Setting>());
        }
    }
}