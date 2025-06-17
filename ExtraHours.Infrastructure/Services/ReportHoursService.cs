using ExtraHours.Core.Models;
using Nager.Date;
using ExtraHours.Core.Repositories;

namespace ExtraHours.Core.Services
{

    public class ReportHoursService
    {
        readonly ISettingRepository _settingRepository;
        readonly IExtraHourTypeRepository _extraHourTypeRepository;
        readonly IExtraHourRepository _extraHourRepository;
        readonly IUserRepository _userRepository;
        public ReportHoursService(ISettingRepository settingRepository, IExtraHourTypeRepository extraHourTypeRepository, IExtraHourRepository extraHourRepository, IUserRepository userRepository)
        {
            _settingRepository = settingRepository;
            _extraHourTypeRepository = extraHourTypeRepository;
            _extraHourRepository = extraHourRepository;
            _userRepository = userRepository;
        }

        public static bool IsHoliday(DateTime date)
        {
            var publicHolidays = HolidaySystem.GetHolidays(date.Year, CountryCode.CO);
            return publicHolidays.Any(x => x.Date == date.Date) || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static List<TimeSpan> ListHours(TimeSpan startHour, TimeSpan endHour)
        {
            TimeSpan rangeHours = endHour - startHour;
            TimeSpan interval = TimeSpan.FromHours(1);
            if (startHour >= endHour)
            {
                throw new ArgumentException("La hora de inicio debe ser menor a la hora de fin.", nameof(startHour));
            }
            if (rangeHours.TotalHours > 2)
            {
                throw new ArgumentException("El rango de horas debe ser mayor a 2 horas.", nameof(rangeHours));
            }
            List<TimeSpan> dayTime = new List<TimeSpan>();
            for (TimeSpan hour = startHour; hour < endHour; hour += interval)
            {
                dayTime.Add(hour);
            }
            return dayTime;
        }

        public HourTypeEnum GetTypeHour(TimeSpan hour, DateOnly date)
        {
            var setting = _extraHourTypeRepository.GetByTypeHourNameAsync("Diurna").Result;

            bool isDayTime = hour >= setting.StartExtraHour && hour <= setting.EndExtraHour;
            bool isHoliday = IsHoliday(date.ToDateTime(TimeOnly.MinValue));

            if (isDayTime)
            {
                return isHoliday ? HourTypeEnum.FestDiurna : HourTypeEnum.Diurna;
            }
            else
            {
                return isHoliday ? HourTypeEnum.FestNocturna : HourTypeEnum.Nocturna;
            }
        }
    }
}