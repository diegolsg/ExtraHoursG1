namespace ExtraHours.Core.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}
