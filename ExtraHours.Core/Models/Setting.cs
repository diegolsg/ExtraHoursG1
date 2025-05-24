namespace ExtraHours.Core.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
        public TimeSpan endHourDay { get; set; }
        public TimeSpan startHourDay { get; set; }
        public int ClosingDay { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;

        public Setting(int id, string key, string value, TimeSpan endHourDay, TimeSpan startHourDay, int closingDay)
        {
            Id = id;
            Key = key;
            Value = value;
            this.endHourDay = endHourDay;
            this.startHourDay = startHourDay;
            ClosingDay = closingDay;
        }
        public Setting()
        {
        }
    }
}
