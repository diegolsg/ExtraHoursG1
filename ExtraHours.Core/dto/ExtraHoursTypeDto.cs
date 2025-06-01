namespace ExtraHours.Core.dto
{
    public class ExtraHourTypeDto
    {
        public int Id { get; set; }
        public required string TypeHourName { get; set; }
        public required string Porcentaje { get; set; }
        public string StartExtraHour { get; set; }
        public string EndExtraHour { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}