using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExtraHours.Core.Models;

namespace ExtraHours.Core.dto
{
    public class ExtraHourDto
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public User users { get; set; } = null!;
        public DateOnly date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        //nuevas
        //public int ExtraHoursTypeId { get; set; }
        //public required ExtraHourType ExtraHoursType { get; set; }

        public ExtraHourDto() { }
        public ExtraHourDto(ExtraHour extraHour)
        {
            Id = extraHour.Id;
            userId = extraHour.userId;
            users = extraHour.users;
            date = extraHour.date;
            StartTime = extraHour.StartTime;
            EndTime = extraHour.EndTime;
            Status = extraHour.Status;
            Created = extraHour.Created;
            Updated = extraHour.Updated;
        }
    }
}
