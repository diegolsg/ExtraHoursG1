namespace ExtraHours.Core.dto
{
    public class ExtraHourSettingsDto
    {
        public SettingDto Setting { get; set; }
        public List<ExtraHourTypeDto> ExtraHourTypes { get; set; }
    }
}