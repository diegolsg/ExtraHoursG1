namespace ExtraHours.Core.Dto
{
    public class SettingsDto
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public required string Value { get; set; }
    }
}