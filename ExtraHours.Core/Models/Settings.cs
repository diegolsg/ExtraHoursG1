namespace ExtraHours.Core.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
