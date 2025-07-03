using ExtraHours.Core.Models;

namespace ExtraHours.Core.dto
{
    public class ExtraHourDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? Users { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;

        public ExtraHourDto() { }
        public ExtraHourDto(ExtraHour extraHour)
        {
            Id = extraHour.Id;
            UserId = extraHour.UserId; 
            Users = extraHour.Users;
            Date = extraHour.Date;
            StartTime = extraHour.StartTime;
            EndTime = extraHour.EndTime;
            Status = extraHour.Status;
            Created = extraHour.Created;
            Updated = extraHour.Updated;
        }
    }
}