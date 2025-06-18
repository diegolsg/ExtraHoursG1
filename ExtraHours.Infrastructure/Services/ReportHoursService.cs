using ExtraHours.Core.Models;
using Nager.Date;
using ExtraHours.Core.Repositories;
using ExtraHours.Core.dto;
using System.Globalization;

namespace ExtraHours.Core.Services
{
    public class ReportHoursService
    {
        readonly IExtraHourTypeRepository _extraHourTypeRepository;
        readonly IExtraHourRepository _extraHourRepository;
        readonly IUserRepository _userRepository;

        public ReportHoursService(
            IExtraHourTypeRepository extraHourTypeRepository,
            IExtraHourRepository extraHourRepository,
            IUserRepository userRepository)
        {
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
            if (startHour >= endHour)
                throw new ArgumentException("La hora de inicio debe ser menor a la hora de fin.");

            TimeSpan rangeHours = endHour - startHour;
            if (rangeHours.TotalHours > 2)
                throw new ArgumentException("El rango de horas debe ser menor o igual a 2 horas.");

            List<TimeSpan> dayTime = new();
            TimeSpan interval = TimeSpan.FromHours(1);
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
                return isHoliday ? HourTypeEnum.FestDiurna : HourTypeEnum.Diurna;
            else
                return isHoliday ? HourTypeEnum.FestNocturna : HourTypeEnum.Nocturna;
        }

        public async Task<List<ReportDto>> GetFullReportAsync()
        {
            var extraHours = await _extraHourRepository.GetAllWithDtoAsync();
            Console.WriteLine($"Total registros recuperados: {extraHours.Count()}");

            var approvedHours = extraHours.Where(x => x.Status?.ToLower() == "aprobado").ToList();
            Console.WriteLine($"Registros aprobados: {approvedHours.Count}");

            var reportByUser = new Dictionary<string, ReportDto>();

            foreach (var extraHour in approvedHours)
            {
                Console.WriteLine($"Procesando hora extra de {extraHour.Code}");

                var user = await _userRepository.GetByCodeAsync(extraHour.Code);
                if (user == null)
                {
                    Console.WriteLine($"Usuario no encontrado: {extraHour.Code}");
                    continue;
                }

                if (!TimeSpan.TryParse(extraHour.StartTime, out var startTime) ||
                !TimeSpan.TryParse(extraHour.EndTime, out var endTime) ||
                !DateTime.TryParseExact(extraHour.Date, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                {
                    Console.WriteLine($"Formato inválido en hora extra del usuario {extraHour.Code}. Fecha recibida: {extraHour.Date}, Hora inicio: {extraHour.StartTime}, Hora fin: {extraHour.EndTime}");
                    continue;
                }

                if (user.Salary <= 0)
                {
                    Console.WriteLine($"Salario inválido para usuario {user.Code}");
                    continue;
                }

                List<TimeSpan> hours;
                try
                {
                    hours = ListHours(startTime, endTime);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error en rango de horas de {user.Code}: {ex.Message}");
                    continue;
                }

                decimal hourlyRate = user.Salary / 240m;
                decimal totalExtraValue = 0m;

                foreach (var hour in hours)
                {
                    var hourType = GetTypeHour(hour, DateOnly.FromDateTime(date));
                    var extraHourType = await _extraHourTypeRepository.GetByTypeHourNameAsync(hourType.ToString());

                    if (!decimal.TryParse(extraHourType.Porcentaje, out decimal porcentaje))
                    {
                        Console.WriteLine($"Porcentaje inválido para tipo {hourType} de {user.Code}");
                        continue;
                    }

                    totalExtraValue += hourlyRate * (porcentaje / 100m);
                }

                if (reportByUser.ContainsKey(user.Code))
                {
                    reportByUser[user.Code].TotalExtraValue += totalExtraValue;
                }
                else
                {
                    reportByUser[user.Code] = new ReportDto
                    {
                        Name = user.Name,
                        Code = user.Code,
                        Salary = user.Salary,
                        TotalExtraValue = totalExtraValue
                    };
                }

                Console.WriteLine($"Usuario {user.Code} -> Total: {totalExtraValue:C}");
            }

            Console.WriteLine("Reporte final generado:");
            foreach (var r in reportByUser.Values)
                Console.WriteLine($"- {r.Name} ({r.Code}): {r.TotalExtraValue:C}");

            return reportByUser.Values.ToList();
        }
    }
}
