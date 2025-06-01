namespace ExtraHours.Core.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public int LimitExtraHoursDay { get; set; }
        public int LimitExtraHoursWeek { get; set; }
        public int TotalHoursWeek { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}