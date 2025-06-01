namespace ExtraHours.Core.dto
{
    public class SettingDto
    {
        public int Id { get; set; }
        public int LimitExtraHoursDay { get; set; }
        public int LimitExtraHoursWeek { get; set; }
        public int TotalHoursWeek { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
