namespace ExtraHours.Core.Dtos
{
    public class ExtraHourDto
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int ExtraHourTypeId { get; set; }
        public string Status { get; set; }
    }
}