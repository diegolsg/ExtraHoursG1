using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.dto;
using ExtraHours.Core.Interfeces.IRepositoties;
using ExtraHours.Core.Models;
using Nager.Date;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExtraHours.Core.Services
{
   
public class SeparateHours
    {
        readonly IRepository<Setting> _settingRepository;
        public SeparateHours(IRepository<Setting> settingRepository)
        {
            _settingRepository = settingRepository;
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
            var setting = _settingRepository.GetById(1).Result;

            bool isDayTime = hour >= setting.startHourDay && hour <= setting.endHourDay;
            bool isHoliday = IsHoliday(date.ToDateTime(TimeOnly.MinValue)); // Convert DateOnly to DateTime

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

